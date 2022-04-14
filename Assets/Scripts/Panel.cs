using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        if (_gameManager != null)
            _gameManager.Pause();
    }
    private void OnDisable()
    {
        if(_gameManager != null)
            _gameManager.Resume();
    }

    public IEnumerator GenerateLosePanel()
    {
        _resumeButton.SetActive(false);

        _text.text = "You Lose...";

        yield return new WaitForSeconds(.5f);

        this.gameObject.SetActive(true);

        yield return null;
    }


}
