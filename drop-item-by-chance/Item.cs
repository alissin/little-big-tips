using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemSO itemSO;

    float currentValue;

    void Start()
    {
        currentValue = itemSO.value;
    }
}