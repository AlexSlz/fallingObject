using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _timerView;
    [SerializeField] private Panel _panel;

    [SerializeField] private int _waitTime = 5;

    private void Start()
    {
        Resume();
    }

    private Coroutine gameStart;
    public void Resume()
    {
        if (this.gameObject.activeSelf)
        {
            gameStart = StartCoroutine(WaitingBeforeStart());
        }
    }


    public void Pause()
    {
        StopCoroutine(gameStart);
        Time.timeScale = 0f;
        _spawner.StopSpawnObject();
        _spawner.SetNonClickableObjects();
    }

    private IEnumerator WaitingBeforeStart()
    {
        for (int i = _waitTime; i > 0; i--)
        {
            _timerView.text = $"{i}";
            yield return new WaitForSecondsRealtime(.5f);
        }
        _timerView.text = "";

        Time.timeScale = 1f;
        _spawner.SetClickableObjects();
        _spawner.StartSpawnObject();
        yield return null;
    }

    public void Lose()
    {
        StartCoroutine(_panel.GenerateLosePanel());
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
