using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthSystem))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _healthText;

    private Animator _healthAnimation;

    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnHealthChange += UpdateBar;
        _healthAnimation = GetComponent<Animator>();
    }

    private void UpdateBar(int current, int max, int speed, float speedMax)
    {
        _healthBar.fillAmount = (float)current / (float)max;
        _healthText.text = $"{current}";
        _healthAnimation.speed = ((float)speed / speedMax) * 2;
    }

}
