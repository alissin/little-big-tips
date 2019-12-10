using UnityEngine;

public class Block : MonoBehaviour {

    bool _isEnqueued = false;
    public bool IsEnqueued {
        get => _isEnqueued;
        set => _isEnqueued = value;
    }

    Block _tail;
    public Block Tail {
        get => _tail;
        set => _tail = value;
    }

    public Vector2Int GetGridPosition() {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }
}