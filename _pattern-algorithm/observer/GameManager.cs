using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    public static GameManager Instance {
        get => _instance;
    }

    [SerializeField]
    Player _player;
    public Player Player {
        get => _player;
    }

    int _killPoints;

    void Awake() {
        _instance = this;
    }

    void Start() {
        _player.OnDieAction += ResetGame;
    }

    void ResetGame() {
        StartCoroutine(ResetGameRoutine());
    }

    IEnumerator ResetGameRoutine() {
        _killPoints = 0;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}