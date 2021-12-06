using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject breakablePlayerPrefab;

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        Instantiate(breakablePlayerPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}