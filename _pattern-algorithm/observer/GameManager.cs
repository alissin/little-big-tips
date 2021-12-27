using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static GameManager Instance
    {
        get => instance;
    }

    [SerializeField]
    Player player;

    public Player Player
    {
        get => player;
    }

    int killPoints;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player.OnDieAction += ResetGame;
    }

    void ResetGame()
    {
        StartCoroutine(ResetGameRoutine());
    }

    IEnumerator ResetGameRoutine()
    {
        killPoints = 0;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}