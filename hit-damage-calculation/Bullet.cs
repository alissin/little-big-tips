using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    AttackSO _attackSO;

    void OnCollisionEnter(Collision collision) {
        CollisionType collisionType = collision.collider.gameObject.GetComponent<CollisionType>();

        if (collisionType != null) {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null) {
                damageable.OnTakeDamage(_attackSO, collisionType.Type);
            }
        }
    }
}