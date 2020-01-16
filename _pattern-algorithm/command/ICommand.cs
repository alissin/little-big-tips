using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand {
    void Execute(Player player);
}

public class UseKnifeCommand : ICommand {

    public void Execute(Player player) {
        player.UseKnife();
    }
}

public class ReloadCommand : ICommand {

    public void Execute(Player player) {
        player.Reload();
    }
}

public class LootCommand : ICommand {

    public void Execute(Player player) {
        player.Loot();
    }
}

public class InteractCommand : ICommand {

    public void Execute(Player player) {
        player.Interact();
    }
}