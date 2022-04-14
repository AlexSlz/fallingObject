using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetProgress()
    {
        _text.text = "";
        Caching.ClearAllCachedVersions("skin");
        PlayerPrefs.DeleteAll();
    }

}
