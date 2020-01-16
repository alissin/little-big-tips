## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > Pattern / Algorithm

### state pattern

Based on this playable demonstration / prototype: [The Dungeon](https://simmer.io/@alissin/the-dungeon).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

_Note_: The purpose of this demonstration is to evaluate this gameplay mechanic. The FPS shooter gameplay mechanic itself, the amazing scenario and the props are free assets from the Asset Store.

> ![The Dungeon](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/the-dungeon.png)

#### Scenario
Terrifying enemy skeletons have a lot of states to deal with.

#### Problem description
How to deal with so many states of the enemy skeleton? For example, if the current state is walk, which state could be the next one? How they are related with each other?

As a classic example (not this case), if the character is jumping, how to avoid the crouch of the character in the air?

Well, you could use a `switch` case to deal with it. But, imagine how many variables and flags you are supposed to have in your class to control it...

#### Solution simplified concept
Based on the _finite state machines_ (or _"FSMs"_) concept, this pattern has each state implemented in an individual and separated class. It makes your code much more clear, organized and easier to deal with.

#### Solution suggestion
In this case, our enemy skeleton has the following states: Resurrection, Idle, Hit, Walk, Attack and Death.<br/>
_Note_: To keep this example simple, we will implement only 3 states: Idle, Walk, Attack. After that, you will get the idea and will be able to implement the other states with no problem.

In the hierarchy, create a game object and name it as `Enemy`:

```
Hierarchy:
- Enemy
```

Create a C# script `EnemyControllerState.cs` and attach this script to the `Enemy` game object:<br/>
_Note_: In this case, I'm using the `NavMesh` system. So make sure to bake the navigation, attach an `Animator` and an `NavMeshAgent` components to the `Enemy` game object.

```csharp
public class EnemyControllerState : MonoBehaviour {
    ...
```

Create a C# script `IEnemyState.cs`. This will be our interface which each state (class) will implement:

```csharp
public interface IEnemyState {

    void Start(EnemyControllerState enemyController);

    IEnemyState Update();
}
```

Create the class of each state and implement the `IEnemyState.cs` interface. You can use the same file (`IEnemyState.cs`):

```csharp
public interface IEnemyState {
    ...
}

public class IdleState : IEnemyState {

    public void Start(EnemyControllerState enemyController) {
        // TODO: implement
    }

    public IEnemyState Update() {
        // TODO: implement
        return null;
    }
}

public class WalkState : IEnemyState {

    public void Start(EnemyControllerState enemyController) {
        // TODO: implement
    }

    public IEnemyState Update() {
        // TODO: implement
        return null;
    }
}

public class AttackState : IEnemyState {

    public void Start(EnemyControllerState enemyController) {
        // TODO: implement
    }

    public IEnemyState Update() {
        // TODO: implement
        return null;
    }
}
```

Now, let's make this work!

Step 1 - in the `EnemyControllerState.cs` script, define the fields and properties:

```csharp
IEnemyState _state;

Animator _animator;
public Animator Animator {
    get => _animator;
}
NavMeshAgent _navMeshAgent;
public NavMeshAgent NavMeshAgent {
    get => _navMeshAgent;
}
```

The `_state` field will be responsible to keep the current state.

Step 2 - start the process:

```csharp
void Start() {
    _animator = GetComponent<Animator>();
    _navMeshAgent = GetComponent<NavMeshAgent>();

    _state = new IdleState();
    _state.Start(this);
}
```

Step 3 - here is where the magic happens:

```csharp
void Update() {
    IEnemyState newState = _state.Update();
    if (newState != null) {
        _state = newState;
        _state.Start(this);
    }
}
```

When the `_state.Update()` method returns a new state, it is when the state is changed. The `_state.Start()` method is like a "setup" or "pre-settings" method of the state.

Step 4 - now, let's implement our first state:

```csharp
public class IdleState : IEnemyState {

    EnemyControllerState _enemyController;
    RaycastHit _hit;
    float _distance = 5.0f;

    public void Start(EnemyControllerState enemyController) {
        _enemyController = enemyController;
    }

    public IEnemyState Update() {
        // check if the Player was seen, via raycast for example
        Ray ray = new Ray(_enemyController.transform.position, _enemyController.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _distance, Color.yellow);
        if (Physics.Raycast(ray, out _hit, _distance)) {
            if (_hit.transform.CompareTag("Player")) {
                return new WalkState();
            }
        }
        return null;
    }
}
```

_Note_: To keep this example simple and focused on the state mechanism, the `Enemy` is looking directly to its front. The [raycast patrol detection](../../raycast-patrol-detection) can help to make it more interesting.

The `IdleState` keeps the `Enemy` patrolling and when the `Player` comes close, it changes the state to the `WalkState`.

Step 5 - let's implement the `WalkState`:

```csharp
public class WalkState : IEnemyState {

    EnemyControllerState _enemyController;

    public void Start(EnemyControllerState enemyController) {
        _enemyController = enemyController;
    }

    public IEnemyState Update() {
        // TODO: get access to the Player position
        _enemyController.NavMeshAgent.SetDestination(GameManager.Instance.Player.transform.position);
        // in this case, I'm using the Blend Tree to control the animations of the Enemy
        _enemyController.Animator.SetFloat("Speed", _enemyController.NavMeshAgent.velocity.magnitude);

        // if the distance of the Enemy and the Player is less than the radius + stoppingDistance of the NavMeshAgent, start the attack!
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, _enemyController.transform.position) <= (_enemyController.NavMeshAgent.stoppingDistance + _enemyController.NavMeshAgent.radius)) {
            return new AttackState();
        }
        
        return null;
    }
}
```

_Note_: In this case, I'm using the [singleton pattern](../singleton) to access the `Player` position. With the `NavMesh` system and the `NavMeshAgent`, we can set the direction of the agent and make it move.

Step 6 - finally, let's implement the `AttackState`:

```csharp
public class AttackState : IEnemyState {

    EnemyControllerState _enemyController;

    public void Start(EnemyControllerState enemyController) {
        _enemyController = enemyController;
        _enemyController.Animator.SetTrigger("Attack");
    }

    public IEnemyState Update() {
        // TODO: get access to the Player transform
        _enemyController.transform.LookAt(GameManager.Instance.Player.transform);
        // keep the X and Z rotations unchanged
        _enemyController.transform.eulerAngles = new Vector3(0, _enemyController.transform.eulerAngles.y, 0);

        // in this case, I'm using the Blend Tree to control the animations of the Enemy. Check if the animation is NOT the Attack animation
        if (_enemyController.animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree")) {
            return new WalkState();
        }
        
        return null;
    }
}
```

As you can see, I'm using the `Animator` to control the current animation from the `Enemy`. So, when the attack animation is finished, the state is changed to the `WalkState` and it tests the distance again. If the `Enemy` is close to the `Player`, it attacks again, otherwise, the mechanism keeps the `WalkState` untill the `Enemy` reachs the `Player`.

#### Scripts:
[EnemyControllerState.cs](./EnemyControllerState.cs), [IEnemyState.cs](./IEnemyState.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [The Dungeon](https://simmer.io/@alissin/the-dungeon).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!