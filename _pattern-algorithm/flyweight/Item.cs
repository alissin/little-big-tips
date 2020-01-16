using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    ItemSO _itemSO;

    Color _randomColor; // TODO: set a random color on Start() method for example

    // Start is called before the first frame update
    void Start() {
        Debug.Log(_itemSO.description + " " + _itemSO.value); // TODO: remove
    }
}