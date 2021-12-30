using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemSO itemSO;

    Color randomColor; // TODO: set a random color on Start() method for example

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(itemSO.description + " " + itemSO.value); // TODO: remove
    }
}