using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour {

    [SerializeField]
    ItemSO[] _itemsSO;

    float _totalDropChance;

    void Start() {
        foreach (var item in _itemsSO) {
            _totalDropChance += item.dropChance;
        }
    }

    public void DropItem(Vector3 dropPosition) {
        float random = Mathf.Round(Random.Range(0, _totalDropChance) * 10.0f) / 10.0f;

        float whichItem = 0.0f;
        foreach (var item in _itemsSO) {
            whichItem += item.dropChance;
            if (whichItem >= random) {
                Instantiate(item.prefab, dropPosition, item.prefab.transform.rotation);
                break;
            }
        }
    }
}