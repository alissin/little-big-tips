using UnityEngine;

public class InputHandler : MonoBehaviour
{
    ICommand qCommand;
    ICommand rCommand;
    ICommand xCommand;
    ICommand kCommand;

    ICommand[] defaultCommands;

    bool isDefaultCommands = true;

    // Start is called before the first frame update
    void Start()
    {
        defaultCommands = new ICommand[4];

        defaultCommands[0] = new UseKnifeCommand();
        defaultCommands[1] = new ReloadCommand();
        defaultCommands[2] = new LootCommand();
        defaultCommands[3] = new InteractCommand();

        qCommand = defaultCommands[0]; // use knife
        rCommand = defaultCommands[1]; // reload
        xCommand = defaultCommands[2]; // loot
        kCommand = defaultCommands[3]; // interact
    }

    // Update is called once per frame
    void Update()
    {
        ICommand command = HandleInput();
        // TODO: get access to the Player script
        command?.Execute(GameManager.Instance.Player);
    }

    ICommand HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            return qCommand;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            return rCommand;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            return xCommand;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            return kCommand;
        }

        return null;
    }

    public void ToggleInput()
    {
        if (isDefaultCommands)
        {
            // alternative commands
            qCommand = defaultCommands[1]; // reload
            rCommand = defaultCommands[2]; // loot
            xCommand = defaultCommands[3]; // interact
            kCommand = defaultCommands[0]; // use knife
        }
        else
        {
            // default commands
            qCommand = defaultCommands[0]; // use knife
            rCommand = defaultCommands[1]; // reload
            xCommand = defaultCommands[2]; // loot
            kCommand = defaultCommands[3]; // interact
        }

        isDefaultCommands = !isDefaultCommands;
    }
}