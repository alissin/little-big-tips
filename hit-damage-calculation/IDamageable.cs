using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void OnTakeDamage(AttackSO attackSO, AttackSO.Type type);
}