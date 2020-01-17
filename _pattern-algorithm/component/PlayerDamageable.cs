using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable {

    float _health = 50.0f;

    public void OnTakeDamage(float amount) {
        if (_health >= Mathf.Epsilon) {
            _health -= amount;
        } else {
            // TODO: death
        }
        Debug.Log("player taking damage..."); // TODO: remove
    }
}