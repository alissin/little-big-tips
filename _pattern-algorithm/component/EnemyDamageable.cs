using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable {

    float _armor = 5.0f;
    float _health = 10.0f;

    public void OnTakeDamage(float amount) {
        if (_armor >= Mathf.Epsilon) {
            _armor -= amount;
        } else if (health >= Mathf.Epsilon) {
            _health -= amount;
        } else {
            // TODO: death
        }
        Debug.Log("enemy taking damage..."); // TODO: remove
    }
}