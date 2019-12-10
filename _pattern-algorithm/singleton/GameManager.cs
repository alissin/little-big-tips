using UnityEngine;

public class GameManager : BaseManager<GameManager> {

    [SerializeField]
    GameObject _player;

    // as we will need only the player position, let's expose only this information
    Vector3 _playerPosition;
    public Vector3 PlayerPosition {
        get => _playerPosition;
    }

    void Update() {
        _playerPosition = _player.transform.position;
    }
}