using UnityEngine;

public class Shake : MonoBehaviour {

    [SerializeField]
    float _frequency = 1000.0f;

    [SerializeField]
    float _amplitude = 10.0f;

    [SerializeField]
    bool _isShaking = false;

    void Update() {
        if (_isShaking) {
            float factor = Mathf.Sin(Time.time * _frequency) * _amplitude;

            float xPos = transform.position.x + factor * Random.Range(-1, 2);
            float yPos = transform.position.y + factor * Random.Range(-1, 2);
            float zPos = transform.position.z + factor * Random.Range(-1, 2);

            transform.position = Vector3.Lerp(transform.position, new Vector3(xPos, yPos, zPos), Time.deltaTime);
        }
    }
}