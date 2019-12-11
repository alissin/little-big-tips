using UnityEngine;

public class Player : MonoBehaviour {

    const float _maxRayGroundedDistance = 2.0f;
    const int _safeLandingAngle = 30;

    bool _isLandingCompleted = false;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Landing Pad")) {
            if (!IsSafeLandingAngle()) {
                // TODO: run player die / explosion method
            }
        } else {
            // TODO: run player die / explosion method
        }
    }

    void Update() {
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxRayGroundedDistance)) {
            if (hit.transform.gameObject.CompareTag("Landing Pad")) {
                Land(hit.transform);
            }
        } else {
            _isLandingCompleted = false;
        }
    }

    public bool IsSafeLandingAngle() {
        return Vector3.Angle(transform.up, Vector3.up) <= _safeLandingAngle;
    }

    void Land(Transform groundTransform) {
        if (!_isLandingCompleted) {
            // check if this ground / landing offset position of the Player makes sense for you. In my case, I used 1.95f
            float landingYPos = groundTransform.position.y + 2.0f;

            Vector3 targetPos = new Vector3(groundTransform.position.x, landingYPos, groundTransform.position.z);

            if (transform.position != targetPos) {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
            } else {
                _isLandingCompleted = true;
                transform.rotation = Quaternion.identity;
            }
        }
    }
}