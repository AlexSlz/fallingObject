using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void EventDelegate(int current, int max, int speed, float speedMax);
    public event EventDelegate OnHealthChange;

    private GameManager _gameManager;
    private Difficulty _difficulty;

    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _scoreNeedToUp = 25;

    private void OnValidate()
    {
        if(_scoreNeedToUp <= 1)
        {
            _scoreNeedToUp = 1;
        }
        if(_maxHealth <= 1) { _maxHealth = 1; }
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _difficulty = FindObjectOfType<Difficulty>();
        _difficulty.OnDifficultyChange += UpdateHealth;
    }

    private void UpdateHealth(object sender, System.EventArgs e)
    {
        int currentDifficulty = Mathf.FloorToInt(Mathf.Clamp(((_difficulty.CurrentDifficulty - 1) / _scoreNeedToUp ) + 1, 1, _maxHealth));
        if (currentDifficulty > _health)
        {
            _health++;
        }
        UpdateView();
    }
    public void Damage()
    {
        _health--;
        _difficulty.DecreaseDifficulty();
        UpdateView();
        if (_health <= 0)
        {
            _gameManager.Lose();
            return;
        }
    }

    private void UpdateView()
    {
        OnHealthChange?.Invoke(_health, _maxHealth, _difficulty.CurrentDifficulty, _difficulty.MaxDifficulty);
    }

}
