using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    float health = 50.0f;

    public void OnTakeDamage(float amount)
    {
        if (health >= Mathf.Epsilon)
        {
            health -= amount;
        }
        else
        {
            // TODO: death
        }

        Debug.Log("player taking damage..."); // TODO: remove
    }
}