using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsSelector : MonoBehaviour
{
    //https://drive.google.com/uc?export=download&id=1Q7v_CklK418w4HOnicSrERG-xWru_3gJ -- web
    //https://drive.google.com/uc?export=download&id=1qnoLbOBnnsXwcEzmNple5oGy8nJsjPKD -- android
    private string _uri = "https://drive.google.com/uc?export=download&id=1qnoLbOBnnsXwcEzmNple5oGy8nJsjPKD";

    [SerializeField] private GameObject _shopItemPrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private Image _loadingBar;

    private static SkinsDatabase _skinsDataBase;
    public static SkinsDatabase SkinsDataBase => _skinsDataBase;

    private void Awake()
    {
        Caching.ClearCache();
    }

    private void Start()
    {
        AssetBundleManager.OnProgressChanged += AssetBundleManager_OnProgressChanged;
    }
    private void OnEnable()
    {
        if (_spawnLocation.childCount <= 0)
        {
            StartCoroutine(GetSkinsDB(skins =>
            {
                _skinsDataBase = skins;

                FillGrid();
            }));
        }
    }
    private void FillGrid()
    {
        int i = 0;
        if (_spawnLocation.childCount <= 0)
        {
            _skinsDataBase?.Skins.ForEach(skin =>
            {
                GameObject temp = Instantiate(_shopItemPrefab, _spawnLocation);
                temp.transform.GetChild(0).GetComponent<Image>().sprite = skin.Sprite;
                temp.name = $"{i++}";
                temp.GetComponentInChildren<Button>().onClick.AddListener(delegate { SetObjectSkin(temp.name); });
            });
            AssetBundleManager.Unload(_uri);
            SetSelectedSkin();
        }
    }

    private void SetSelectedSkin()
    {
        foreach (Transform child in _spawnLocation.transform)
        {
            child.gameObject.GetComponent<Image>().enabled = false;
        }
        _spawnLocation.transform.GetChild(PlayerPrefs.GetInt("SkinId")).GetComponent<Image>().enabled = true;
    }

    private void SetObjectSkin(string _id)
    {
        int id = int.Parse(_id);
        PlayerPrefs.SetInt("SkinId", id);
        SetSelectedSkin();
    }


    public IEnumerator GetSkinsDB(System.Action<SkinsDatabase> callBack)
    {
        AssetBundle bundle = AssetBundleManager.getAssetBundle(_uri);
        if (!bundle)
        {
            yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(_uri));
        }

        bundle = AssetBundleManager.getAssetBundle(_uri);

        SkinsDatabase skinsDatabase = bundle.LoadAsset<SkinsDatabase>("SkinsDB");

        _loadingBar.transform.parent.parent.gameObject.SetActive(false);

        callBack(skinsDatabase);
    }
    private void AssetBundleManager_OnProgressChanged(float proggress)
    {
        if (!_loadingBar.IsDestroyed())
        {
            _loadingBar.fillAmount = proggress;
        }
    }
}
