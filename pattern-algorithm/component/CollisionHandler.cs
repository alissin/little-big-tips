using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        IAttackable attackable = GetComponent<IAttackable>();
        if (attackable != null)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                attackable.OnAttack(damageable);
            }
        }
    }
}