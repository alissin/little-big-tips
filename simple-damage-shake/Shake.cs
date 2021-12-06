using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 0.5f)]
    float range = 0.2f;

    [SerializeField]
    bool isShaking = false;

    Vector3 lastPos;
    bool hasLastPosHeld = false;

    void Update()
    {
        if (isShaking)
        {
            if (!hasLastPosHeld)
            {
                lastPos = transform.position;
                hasLastPosHeld = true;
            }

            float xPos = lastPos.x + Random.Range(-range, range);
            float yPos = lastPos.y + Random.Range(-range, range);
            float zPos = lastPos.z + Random.Range(-range, range);

            transform.position = new Vector3(xPos, yPos, zPos);
        }
        else
        {
            if (hasLastPosHeld)
            {
                transform.position = lastPos;
                hasLastPosHeld = false;
            }
        }
    }
}