 using UnityEngine;
 
 public class Enemy : MonoBehaviour {

    Vector3 _defaultPos;

    void Start() {
        _defaultPos = transform.position;
    }

    // TODO: call this method when you do not need the object anymore and want to return it to the object pool
    public void OnHide() {
        // reset the object default position
        transform.position = _defaultPos;

        // TODO: you will need a reference of the `Spawn Controller` script
        _spawnController.ReturnObjToPool(gameObject);
    }
}