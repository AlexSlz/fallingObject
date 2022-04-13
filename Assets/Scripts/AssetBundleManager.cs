using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetBundleManager
{
    private static Dictionary<string, AssetBundle> _bundles = new Dictionary<string, AssetBundle>();

    public delegate void EventDelegate(float proggress);
    public delegate void EventDelegateEmpty();

    public static event EventDelegate OnProgressChanged;
    public static event EventDelegateEmpty OnLoadError;

    public static AssetBundle getAssetBundle(string url)
    {
        return _bundles.TryGetValue(url, out AssetBundle bundle) ? bundle : null;
    }

    public static IEnumerator downloadAssetBundle(string url)
    {
        if (_bundles.ContainsKey(url))
            yield return null;
        else
        {
            while (!Caching.ready)
                yield return null;

            using (WWW request = WWW.LoadFromCacheOrDownload(url, 0))
            {
                while (!request.isDone)
                {
                    OnProgressChanged?.Invoke(request.progress);
                    yield return null;
                }
                if (request.error == null)
                {
                    _bundles.Add(url, request.assetBundle);
                }
                else
                {
                    OnLoadError?.Invoke();
                    throw new Exception(request.error);
                }
            }
        }
    }
    public static void Unload(string url, bool allObjects = false)
    {
        if (_bundles.TryGetValue(url, out AssetBundle bundle))
        {
            bundle.Unload(allObjects);
            _bundles.Remove(url);
        }
    }
}
