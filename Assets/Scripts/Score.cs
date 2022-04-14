using UnityEngine;

public class Score : MonoBehaviour
{
    public delegate void EventDelegate(int score);

    public event EventDelegate OnScoreChanged;
    public event EventDelegate OnHighScoreChanged;

    private int _score = 0;
    private int _highScore = 0;

    private void Start()
    {
        Add(0);
    }

    public void Add(int count)
    {
        int multiplier = PlayerPrefs.GetInt("ScoreMultiplier");
        _score += count * ((multiplier > 0) ? multiplier : 1);
        if(_score >= _highScore) { 
            ChangeHightScore();
        }
        OnScoreChanged?.Invoke(_score);
    }

    private void ChangeHightScore()
    {
        _highScore = (_score == 0) ? PlayerPrefs.GetInt("HighScore") : _score;
        PlayerPrefs.SetInt("HighScore", _highScore);
        OnHighScoreChanged?.Invoke(_highScore);
    }
}
