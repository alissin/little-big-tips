using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 200.0f;

    [SerializeField]
    float rotationFactor = 0.015f;

    float lh;

    void Update()
    {
        lh = Input.GetAxis("Horizontal");

        transform.RotateAround(Vector3.zero, Vector3.up, lh * Time.deltaTime * moveSpeed);

        float rotation = rotationFactor * moveSpeed;
        transform.Rotate(Vector3.forward, -lh * Time.deltaTime * moveSpeed * rotation);
    }
}