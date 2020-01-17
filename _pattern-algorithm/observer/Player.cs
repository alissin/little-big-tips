using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Action OnDieAction;

    float _health = 100.0f;

    void Start() {
        OnDieAction += RunExplosion;
    }

    public void OnTakeDamage(float amount) {
        _health -= amount;
        if (_health <= Mathf.Epsilon) {
            OnDieAction.Invoke();
        }
    }

    void RunExplosion() {
        Debug.Log("Boom!!!"); // TODO: impl. a big explosion
    }
}