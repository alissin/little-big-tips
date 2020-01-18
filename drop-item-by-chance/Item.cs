using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    ItemSO _itemSO;

    float _currentValue;

    void Start() {
        _currentValue = _itemSO.value;
    }
}