using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    float armor = 5.0f;
    float health = 10.0f;

    public void OnTakeDamage(AttackSO attackSO, AttackSO.Type type)
    {
        if (armor > 0)
        {
            float armorDamage = attackSO.CalculateDamage(type, AttackSO.Phase.Armor);
            armor -= armorDamage;
            // TODO: impl. update the armor on UI
            Debug.LogFormat("Damage on {0} (Armor) -> amount: {1}", type, armorDamage);
            return;
        }

        float healthDamage = attackSO.CalculateDamage(type, AttackSO.Phase.Health);
        health -= healthDamage;
        // TODO: impl. update the health on UI
        Debug.LogFormat("Damage on {0} (Health) -> amount: {1}", type, healthDamage);
        if (health <= Mathf.Epsilon)
        {
            // TODO: impl. Enemy's death
        }
    }
}