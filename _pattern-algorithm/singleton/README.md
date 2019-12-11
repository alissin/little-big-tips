## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > Pattern / Algorithm

### singleton pattern

Based on this demonstration / prototype: [Combat Wings](https://simmer.io/@alissin/combat-wings)

> ![Combat Wings](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/combat-wings.png)

#### Scenario
When the player airplane gets closer to the enemy, the attack starts!

#### Problem description
There are several enemies on this level and all of them need to know where is the player airplane and check if it is close enough to start the attack.

#### Solution simplified concept
It garantees a unique global instance of it and we can access it whenever we need, wherever we are. Although singleton pattern is not likely recommended to big games, for simple ones, sometimes, it can worth.

#### Solution suggestion
In this case a singleton instance of a class will keep the reference of the player airplane, more specifically its position and the enemy can check the distance between then to start the attack.<br/>
_Note_: the scope of this _**Little Big Tip**_ is only the singleton pattern.

In the hierarchy, create a game object and name it as `Game Manager`:

```
Hierarchy:
- Game Manager
```

Create a C# script `GameManager.cs` and attach this script to the `Game Manager` game object:

```csharp
public class GameManager : MonoBehaviour {
    ...
```

Define the fields:

```csharp
// will keep the singleton instance of this object
static GameManager _instance;
public static GameManager Instance {
    get => _instance;
}

[SerializeField]
GameObject _player;

// as we will need only the player position, let's expose only this information
Vector3 _playerPosition;
public Vector3 PlayerPosition {
    get => _playerPosition;
}
```

Don't forget to set the field `_player` on `Game Manager` game object via inspector.

Step 1 - create the singleton instance:

```csharp
void Awake() {
    _instance = this;
}
```

Step 2 - make sure the player's position is always up-to-date:

```csharp
void Update() {
    _playerPosition = _player.transform.position;
    ...
}
```

Step 3 - cool! Now, we can access this information whenever we need, wherever we are:

```csharp
void CheckPlayerPosition() {
    Vector3 playerPosition = GameManager.Instance.PlayerPosition;
    ...
}
```

Ok. Now imagine that we need more singletons classes. Come on, duplicate the code? Will we need to have the `_instance` field declaration, the instantiation code (`_instance = this`) on `Awake()` method in Every singleton class? No way! If we will use a singleton, let's use a... more "elegant" singleton then!

_Note_: We will use [Generics](https://en.wikipedia.org/wiki/Generic_programming) in this case.

Step 4 - let's make it better! Create a C# script `BaseManager.cs` (abstract class):

```csharp
public abstract class BaseManager<T> : MonoBehaviour where T : BaseManager<T> {
    static T _instance;

    public static T Instance {
        get {
            // lazy instantiation, in a situation that you need the singleton in runtime but it was not yet instantiated
            if (_instance == null) {
                GameObject itemClone = new GameObject(typeof(T).Name);
                T manager = itemClone.AddComponent<T>();
                _instance = manager;
            }
            return _instance;
        }
        set {
            if (_instance == null) {
                _instance = value;
                DontDestroyOnLoad(_instance.gameObject);
            } else if (_instance != value) {
                // if for some reason, you have duplication, destroy the duplicated instance
                Destroy(value.gameObject);
            }
        }
    }

    void Awake() {
        Instance = this as T;
    }
}
```

You can see that the field `_instance`, the property `Instance` and the `_instance = this` from `Awake()` method were moved to the `BaseManager.cs`.

Step 5 - make the `GameManager.cs` extends the `BaseManager.cs`, remove the field `_instance`, remove the property `Instance`, remove the `_instance = this` from `Awake()` method from `GameManager.cs`:

```csharp
public class GameManager : BaseManager<GameManager> {
    ...
```

Pretty cool! Now, if we want to create another singleton class, for example a `UIManager.cs` or `SoundManager.cs`, we just need to extend `BaseManager.cs` and we are done!

#### Classes:
[BaseManager.cs](./BaseManager.cs), [GameManager.cs](./GameManager.cs)

More _**Little Big Tips**_? Nice, [follow me](https://github.com/alissin/little-big-tips)!