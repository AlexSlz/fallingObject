using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Area : MonoBehaviour
{
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = FindObjectOfType<HealthSystem>();
    }
    [SerializeField]
    private bool DestroyArea = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Object obj = collision.gameObject.GetComponent<Object>();
        if (DestroyArea)
        {
            if (!obj.PlayingAnimation)
            {
                obj.PlayDestroyAnimation();
                _healthSystem.Damage();
            }
        }
        else
        {
            collision.gameObject.GetComponent<Object>().CanClick = true;
        }
    }
}
