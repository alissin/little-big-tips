using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnDieAction;

    float health = 100.0f;

    void Start()
    {
        OnDieAction += RunExplosion;
    }

    public void OnTakeDamage(float amount)
    {
        health -= amount;
        if (health <= Mathf.Epsilon)
        {
            OnDieAction.Invoke();
        }
    }

    void RunExplosion()
    {
        Debug.Log("Boom!!!"); // TODO: impl. a big explosion
    }
}