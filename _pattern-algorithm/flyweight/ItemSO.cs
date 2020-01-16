using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]
public class ItemSO : ScriptableObject {

    public enum Type {
        MagHandgun, Grenade, HealthPotion
    }

    public Type type;
    public float value;
    public string description;
}