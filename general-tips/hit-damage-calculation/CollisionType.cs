using UnityEngine;

public class CollisionType : MonoBehaviour
{
    [SerializeField]
    AttackSO.Type type;

    public AttackSO.Type Type
    {
        get => type;
    }
}