using UnityEngine;

public class Block : MonoBehaviour
{
    bool isEnqueued = false;

    public bool IsEnqueued
    {
        get => isEnqueued;
        set => isEnqueued = value;
    }

    Block tail;

    public Block Tail
    {
        get => tail;
        set => tail = value;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }
}