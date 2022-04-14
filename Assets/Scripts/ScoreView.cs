using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreView;
    [SerializeField] private TMP_Text _highScoreView;

    private Score _score;

    private void Awake()
    {
        _score = GetComponent<Score>();
        SetHighScore(PlayerPrefs.GetInt("HighScore"));
    }

    private void OnEnable()
    {
        if (_score != null)
        {
            _score.OnScoreChanged += SetScore;
            _score.OnHighScoreChanged += SetHighScore;
        }
    }

    private void SetScore(int score)
    {
        _scoreView.text = $"{score}";
    }

    private void SetHighScore(int score)
    {
        _highScoreView.text = $"HightScore: {score}";
    }

}
