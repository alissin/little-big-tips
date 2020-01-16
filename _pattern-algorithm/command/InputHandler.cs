using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    ICommand _qCommand;
    ICommand _rCommand;
    ICommand _xCommand;
    ICommand _kCommand;

    ICommand[] _defaultCommands;

    bool _isDefaultCommands = true;

    // Start is called before the first frame update
    void Start() {
        _defaultCommands = new ICommand[4];

        _defaultCommands[0] = new UseKnifeCommand();
        _defaultCommands[1] = new ReloadCommand();
        _defaultCommands[2] = new LootCommand();
        _defaultCommands[3] = new InteractCommand();

        _qCommand = _defaultCommands[0]; // use knife
        _rCommand = _defaultCommands[1]; // reload
        _xCommand = _defaultCommands[2]; // loot
        _kCommand = _defaultCommands[3]; // interact
    }

    // Update is called once per frame
    void Update() {
        ICommand command = HandleInput();
        // TODO: get access to the Player script
        command?.Execute(GameManager.Instance.Player);
    }

    ICommand HandleInput() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            return _qCommand;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            return _rCommand;
        } else if (Input.GetKeyDown(KeyCode.X)) {
            return _xCommand;
        } else if (Input.GetKeyDown(KeyCode.K)) {
            return _kCommand;
        }

        return null;
    }

    public void ToggleInput() {
        if (_isDefaultCommands) {
            // alternative commands
            _qCommand = _defaultCommands[1]; // reload
            _rCommand = _defaultCommands[2]; // loot
            _xCommand = _defaultCommands[3]; // interact
            _kCommand = _defaultCommands[0]; // use knife
        } else {
            // default commands
            _qCommand = _defaultCommands[0]; // use knife
            _rCommand = _defaultCommands[1]; // reload
            _xCommand = _defaultCommands[2]; // loot
            _kCommand = _defaultCommands[3]; // interact
        }
        _isDefaultCommands = !_isDefaultCommands;
    }
}