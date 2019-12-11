using UnityEngine;

public class BreakablePlayer : MonoBehaviour {

    Vector3[] _explosionDirections;

    void Start() {
        _explosionDirections = new Vector3[] { Vector3.up, Vector3.right, -Vector3.one };

        // get all the childs ("breakable" pieces) and apply the Physics force individually on each one
        for (int i = 0; i < transform.childCount - 1; i++) {
            ImpulseByExplosion(transform.GetChild(i), i);
        }
    }

    void ImpulseByExplosion(Transform transform, int idx) {
        float factor = Random.Range(0.2f, 1.0f);
        float explosionImpulse = Random.Range(50.0f, 100.0f);
        Vector3 force = _explosionDirections[idx % _explosionDirections.Length] * factor * explosionImpulse;
        transform.GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Impulse);
    }
}