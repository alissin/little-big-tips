using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour, IAttackable {

    [SerializeField]
    AttackSO _attack;

    public void OnAttack(IDamageable damageable) {
        damageable.OnTakeDamage(_attack.baseDamage);
    }
}