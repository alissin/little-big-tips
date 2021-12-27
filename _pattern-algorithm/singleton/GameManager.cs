using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField]
    GameObject player;

    // as we will need only the player position, let's expose only this information
    Vector3 playerPosition;

    public Vector3 PlayerPosition
    {
        get => playerPosition;
    }

    void Update()
    {
        playerPosition = player.transform.position;
    }
}