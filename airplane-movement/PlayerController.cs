using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    [Range(5.0f, 20.0f)]
    float _movementSpeed = 8.0f;

    [SerializeField]
    float _pitchFactor = 2.5f;

    [SerializeField]
    float _yawFactor = 3.0f;

    [SerializeField]
    float _rotationFactor = 20.0f;

    [SerializeField]
    float _rollFactor = 15.0f;

    const float _maxHorRangePosition = 5.0f;
    const float _maxVerRangePosition = 4.0f;

    float _lh, _lv;

    void Update() {
        _lh = Input.GetAxis("Horizontal");
        _lv = Input.GetAxis("Vertical");

        SetupPosition();
        SetupRotation();
    }

    void SetupPosition() {
        // horizontal and vertical offset
        float realTimeMovementSpeed = _movementSpeed * Time.deltaTime;
        float lhOffset = _lh * realTimeMovementSpeed;
        float lvOffset = _lv * realTimeMovementSpeed;

        // clamp the values to limit the screen
        float xPos = Mathf.Clamp(lhOffset + transform.localPosition.x, -_maxHorRangePosition, _maxHorRangePosition);
        float yPos = Mathf.Clamp(-lvOffset + transform.localPosition.y, -_maxVerRangePosition, _maxVerRangePosition);

        // localPosition because `Player` is a child of the Main Camera
        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }

    void SetupRotation() {
        float xRot = transform.localPosition.y * -_pitchFactor + _lv * _rotationFactor;
        float yRot = transform.localPosition.x * _yawFactor + _lh * _rotationFactor;
        float zRot = -_lh * _rollFactor;

        transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
    }
}