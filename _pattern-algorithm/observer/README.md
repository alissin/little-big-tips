## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > Pattern / Algorithm > observer pattern

> ![Realm Defender](./../../z_images/realm_defender/observer.png)

Feel free to try this behaviour on the playable demonstration / prototype: [Realm Defender](https://simmer.io/@alissin/realm-defender).<br/>
<sub>_Note_: The purpose of this demonstration is to evaluate this gameplay mechanic. The scenario and the props are free assets from the Asset Store.</sub>

#### Scenario
In this case, our player is the strong (or not enough) blue castle! When the health is gone, the player "dies" and so many parts of the game should know about that, right?

#### Problem description
When the player's health is gone, the player "dies" and so many parts of the game should know about that. We need to notify all game objects somehow. We could call each method individually in sequency but, imagine in a big game, many places to put the same code sequency and worse, we easily could make inconsistencies.

#### Solution simplified concept
With the _observer_ pattern, we can declare a delegate and who wants to know something about the event, just need to subscribe on it.

#### Solution suggestion
In this case, the Game Manager (responsible for centralize the main mechanics like reset and restart the game) and the UI Manager (responsible for control the UI stuff) should be notified on player's death.

In the hierarchy, create 3 game objects and name them as `Player`, `Game Manager` and `UI Manager`:

```
Hierarchy:
- Player
- Game Manager
- UI Manager
```

Create a C# script `Player.cs` and attach this script to the `Player` game object:

```csharp
public class Player : MonoBehaviour
{
    ...
```

Create a C# script `GameManager.cs` and attach this script to the `Game Manager` game object:

```csharp
public class GameManager : MonoBehaviour
{
    ...
```

Create a C# script `UIManager.cs` and attach this script to the `UI Manager` game object:

```csharp
public class UIManager : MonoBehaviour
{
    ...
```

In the `Player.cs` script, define the fields:

```csharp
public Action OnDieAction;

float health = 100.0f;
```

The `OnDieAction` will be our `delegate` (`Action`) object and to use that, we need to put the `System` namespace in our class like so: `using System`.

First, let's create the methods of the `Player.cs`:

```csharp
public void OnTakeDamage(float amount)
{
    health -= amount;
    if (health <= Mathf.Epsilon)
    {
        // TODO: trigger the action
    }
}

void RunExplosion()
{
    Debug.Log("Boom!!!"); // TODO: impl. a big explosion
}
```

Subscribe on it's own `delegate` (`Action`):

```csharp
void Start()
{
    OnDieAction += RunExplosion;
}
```

It means that when the `OnDieAction` is invoked, the method `RunExplosion()` will be called automatically.

Now, let's make this more fun and make another classes to subscribe on it as well.

In the `GameManager.cs` script, define the fields and methods:<br/>
<sub>_Note_: In this case, I'm using the [singleton pattern](../singleton) to access the `Player` script. To use the `SceneManager`, we need to put the `UnityEngine.SceneManagement` namespace in this class like so: `using UnityEngine.SceneManagement`.</sub>

```csharp
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
```

Now, subscribe on the `OnDieAction` from the `Player`:

```csharp
void Start()
{
    player.OnDieAction += ResetGame;
}
```

Now, we have the `Player.cs` itself and the `GameManager.cs` subscribed on the `OnDieAction`. It means that, when the `OnDieAction` is invoked, the method `RunExplosion()` and `ResetGame()` will be called automatically.

Let's finish this with our `UIManager.cs`:<br/>
<sub>_Note:_ Don't forget to use the namespace `using UnityEngine.UI` (or `using TMPro`, in case you are using `TextMeshProUGUI`):</sub>

```csharp
[SerializeField]
Text statusText;

public void ShowGameOverMenu()
{
    statusText.text = "You lose!";
    statusText.gameObject.SetActive(true);
}
```

Subscribe on the `OnDieAction` from the `Player`:

```csharp
void Start()
{
    GameManager.Instance.Player.OnDieAction += ShowGameOverMenu;
}
```

Now, the final step: invoke the action to notify all subscribers. In the `Player.cs` script, when the health is gone:

```csharp
public void OnTakeDamage(float amount)
{
    health -= amount;
    if (health <= Mathf.Epsilon)
    {
        OnDieAction.Invoke();
    }
}
```

Cool! At this moment, we have the `Player.cs` itself, the `GameManager.cs` and the `UIManager.cs` subscribed on the `OnDieAction`. It means that, when the `OnDieAction` is invoked, I mean, when the `Player` dies, every object subscribed on this `delegate` will be notified. So the methods `RunExplosion()`, `ResetGame()` and `ShowGameOverMenu()` will be called automatically.

To try the behaviour of the pattern, put this line somewhere to force the `Player`'s death:

```csharp
player.OnTakeDamage(100.0f);
```

#### Scripts:
[Player.cs](./Player.cs), [GameManager.cs](./GameManager.cs), [UIManager.cs](./UIManager.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Realm Defender](https://simmer.io/@alissin/realm-defender).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!