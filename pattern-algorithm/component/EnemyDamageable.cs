using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    float armor = 5.0f;
    float health = 10.0f;

    public void OnTakeDamage(float amount)
    {
        if (armor >= Mathf.Epsilon)
        {
            armor -= amount;
        }
        else if (health >= Mathf.Epsilon)
        {
            health -= amount;
        }
        else
        {
            // TODO: death
        }

        Debug.Log("enemy taking damage..."); // TODO: remove
    }
}