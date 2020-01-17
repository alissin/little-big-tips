using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    [Range(1.0f, 5.0f)]
    float _rayDistanceForward = 5.0f;

    [SerializeField]
    [Range(1.0f, 5.0f)]
    float _rayDistanceSide = 3.0f;

    [SerializeField]
    [Range(1.0f, 5.0f)]
    float _rayDistanceBack = 1.0f;

    RaycastHit _hit;

    int _layerMaskExceptEnemy;

    void Start() {
        // Bit shift the index of the layer (9 - Enemy) to get a bit mask. This would cast rays only against colliders in layer 9.
        int _enemyLayerMask = 1 << 9;

        // But instead we want to collide against everything except layer 9. The ~ operator does this, it inverts a bitmask.
        _layerMaskExceptEnemy = ~_enemyLayerMask;

        StartCoroutine(RaycastTargetRoutine());
    }

    IEnumerator RaycastTargetRoutine() {
        float timeOffset = Time.time;
        while (true) {
            // default init: x = 0 / z = 1
            float frequency = 5.0f; // more than 5 
            float x = Mathf.Round(Mathf.Sin((Time.time - timeOffset) * frequency) * 10) / 10;
            float z = Mathf.Round(Mathf.Sin((Time.time - timeOffset + 1.0f) * frequency) * 10) / 10;
            Vector3 rayDirection = new Vector3(x, 0, z);

            float distance;
            float dotProduct = Vector3.Dot(transform.forward, rayDirection);
            if (dotProduct >= Mathf.Epsilon) {
                distance = Mathf.SmoothStep(_rayDistanceSide, _rayDistanceForward, dotProduct);
            } else {
                distance = Mathf.SmoothStep(_rayDistanceSide, _rayDistanceBack, -dotProduct); // invert because smoothstep runs with 0 to 1
            }

            RaycastTarget(rayDirection, distance);
            yield return null;
        }
    }

    void RaycastTarget(Vector3 rayDirection, float distance) {
        Ray ray = new Ray(transform.position, rayDirection);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);

        if (Physics.Raycast(ray, out _hit, distance, _layerMaskExceptEnemy)) {
            // TODO: impl. the Player was seen
        }
    }
}