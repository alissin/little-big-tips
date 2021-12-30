using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    Text statusText;

    void Start()
    {
        GameManager.Instance.Player.OnDieAction += ShowGameOverMenu;
    }

    public void ShowGameOverMenu()
    {
        statusText.text = "You lose!";
        statusText.gameObject.SetActive(true);
    }
}