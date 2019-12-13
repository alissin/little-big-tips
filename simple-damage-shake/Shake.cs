using UnityEngine;

public class Shake : MonoBehaviour {

    [SerializeField]
    [Range(0.1f, 0.5f)]
    float _range = 0.2f;

    [SerializeField]
    bool _isShaking = false;

    Vector3 _lastPos;
    bool _hasLastPosHeld = false;

    void Update() {
        if (_isShaking) {
            if (!_hasLastPosHeld) {
                _lastPos = transform.position;
                _hasLastPosHeld = true;
            }

            float xPos = _lastPos.x + Random.Range(-_range, _range);
            float yPos = _lastPos.y + Random.Range(-_range, _range);
            float zPos = _lastPos.z + Random.Range(-_range, _range);

            transform.position = new Vector3(xPos, yPos, zPos);
        } else {
            if (_hasLastPosHeld) {
                transform.position = _lastPos;
                _hasLastPosHeld = false;
            }
        }
    }
}