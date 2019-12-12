using UnityEngine;

public class Block : MonoBehaviour {

    public Vector3 GetPosition() {
        return new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
    }
}