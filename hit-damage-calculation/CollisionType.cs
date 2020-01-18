using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionType : MonoBehaviour {

    [SerializeField]
    AttackSO.Type _type;
    public AttackSO.Type Type {
        get => _type;
    }
}