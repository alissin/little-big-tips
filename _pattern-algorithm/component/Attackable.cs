using UnityEngine;

public class Attackable : MonoBehaviour, IAttackable
{
    [SerializeField]
    AttackSO attack;

    public void OnAttack(IDamageable damageable)
    {
        damageable.OnTakeDamage(attack.baseDamage);
    }
}