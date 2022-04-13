using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class Difficulty : MonoBehaviour
{
    public event System.EventHandler OnDifficultyChange;


    [SerializeField]
    private AnimationCurve _difficultyScale;

    public float MaxDifficulty => _difficultyScale.keys[_difficultyScale.length - 1].time;

    [SerializeField]
    private int _difficultyScaler;

    public int CurrentDifficulty => _difficultyScaler; 

    [SerializeField]
    private int _difficultyScalerMultiplier = 2;

    [SerializeField]
    private float _spawnSpeedMultiplier = 1.5f;


    private Spawner _spawner;
    private Score _score;
    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _score = FindObjectOfType<Score>();
        _score.OnScoreChanged += ChangeDifficulty;
    }
    private void ChangeDifficulty(int score)
    {
        _difficultyScaler += _difficultyScalerMultiplier;
        float FallingSpeed = _difficultyScale.Evaluate((float)_difficultyScaler);
        float SpawningSpeed = _difficultyScale.keys[_difficultyScale.length - 1].value - _spawnSpeedMultiplier - FallingSpeed;

        _spawner.setSpeed(FallingSpeed, SpawningSpeed);
        OnDifficultyChange?.Invoke(this, System.EventArgs.Empty);
    }

    public void DecreaseDifficulty()
    {
        _difficultyScaler /= 2;
    }
}
