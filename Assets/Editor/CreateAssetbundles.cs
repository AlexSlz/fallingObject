using System.IO;
using UnityEditor;


public class CreateAssetbundles
{
    static string dir = "AssetBundles";
    [MenuItem("Assets/Build AssetBundles/Windows")]
    static void BuildAllAssetBundlesWindows()
    {
        CreateFolder();
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

    }

    [MenuItem("Assets/Build AssetBundles/Android")]
    static void BuildAllAssetBundlesAndroid()
    {
        CreateFolder();
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("Assets/Build AssetBundles/Web")]
    static void BuildAllAssetBundlesWeb()
    {
        CreateFolder();
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    }

    static void CreateFolder()
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

}