using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(5.0f, 20.0f)]
    float movementSpeed = 8.0f;

    [SerializeField]
    float pitchFactor = 2.5f;

    [SerializeField]
    float yawFactor = 3.0f;

    [SerializeField]
    float rotationFactor = 20.0f;

    [SerializeField]
    float rollFactor = 15.0f;

    const float maxHorRangePosition = 5.0f;
    const float maxVerRangePosition = 4.0f;

    float lh, lv;

    void Update()
    {
        lh = Input.GetAxis("Horizontal");
        lv = Input.GetAxis("Vertical");

        SetupPosition();
        SetupRotation();
    }

    void SetupPosition()
    {
        // horizontal and vertical offset
        float realTimeMovementSpeed = movementSpeed * Time.deltaTime;
        float lhOffset = lh * realTimeMovementSpeed;
        float lvOffset = lv * realTimeMovementSpeed;

        // clamp the values to limit the screen
        float xPos = Mathf.Clamp(lhOffset + transform.localPosition.x, -maxHorRangePosition, maxHorRangePosition);
        float yPos = Mathf.Clamp(-lvOffset + transform.localPosition.y, -maxVerRangePosition, maxVerRangePosition);

        // localPosition because `Player` is a child of the Main Camera
        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }

    void SetupRotation()
    {
        float xRot = transform.localPosition.y * -pitchFactor + lv * rotationFactor;
        float yRot = transform.localPosition.x * yawFactor + lh * rotationFactor;
        float zRot = -lh * rollFactor;

        transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
    }
}