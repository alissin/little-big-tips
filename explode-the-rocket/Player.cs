using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    GameObject _breakablePlayerPrefab;

    void OnCollisionEnter(Collision collision) {
        Explode();
    }

    void Explode() {
        Instantiate(_breakablePlayerPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}