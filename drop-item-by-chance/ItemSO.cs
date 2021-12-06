using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public GameObject prefab;
    public float value;
    public float dropChance;
    public string description;
}