using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    float _moveSpeed = 200.0f;

    [SerializeField]
    float _rotationFactor = 0.015f;

    float _lh;

    void Update () {
        _lh = Input.GetAxis("Horizontal");

        transform.RotateAround(Vector3.zero, Vector3.up, _lh * Time.deltaTime * _moveSpeed);

        float rotationFactor = _rotationFactor * _moveSpeed;
        transform.Rotate(Vector3.forward, -_lh * Time.deltaTime * _moveSpeed * rotationFactor);
    }
}