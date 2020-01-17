using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    Text _statusText;

    void Start() {
        GameManager.Instance.Player.OnDieAction += ShowGameOverMenu;
    }

    public void ShowGameOverMenu() {
        _statusText.text = "You lose!";
        _statusText.gameObject.SetActive(true);
    }
}