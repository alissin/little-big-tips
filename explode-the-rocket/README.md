## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > General tips

### explode the rocket

Based on this playable demonstration / prototype: [Boost the Rocket](https://simmer.io/@alissin/boost-the-rocket).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

> ![Boost the Rocket](./boost-the-rocket-explosion.png)

#### Scenario
Don't touch anything on this dangerous space dungeons because you can explode yourself!

#### Problem description
The visual effect is very important and bring more realism and fun to the game. So, when the rocket explode, all the pieces should fly away.

#### Solution simplified concept
When the explosion happens, we will use a "breakable" game object and apply the Physics force to each piece to different directions.

#### Solution suggestion
In this case, we have 2 game objects (Prefabs): the `Player` and the `Breakable Player`. When the `Player` hits something, it's destroyed and immediatly the `Breakable Player` is instantiated at the same position and rotation and the Physics force is applied to it, individually on each "breakable" piece.

Create a C# script `Player.cs` and attach this script to the `Player` game object:

```csharp
public class Player : MonoBehaviour {
    ...
```

Define the fields:

```csharp
[SerializeField]
GameObject _breakablePlayerPrefab;
```

Step 1 - call the `Explode()` method when the `Player` dies. How to call this method, it's up to you. In this case, it's called when the `Player` hits something, on `OnCollisionEnter()` method. In the `Player.cs` script:

```csharp
void OnCollisionEnter(Collision collision) {
    Explode();
}

void Explode() {
    Instantiate(_breakablePlayerPrefab, transform.position, transform.rotation);
    Destroy(gameObject);
}
```

Create a C# script `BreakablePlayer.cs`, attach this script to the `Breakable Player` game object, create a Prefab of it and remove it from the scene:

```csharp
public class BreakablePlayer : MonoBehaviour {
    ...
```

Define the fields:

```csharp
Vector3[] _explosionDirections;
```

Don't forget to set the field `_breakablePlayerPrefab` on `Player` game object via inspector.

Step 2 - when we instantiate the `Breakable Player` game object, the `BreakablePlayer.Start()` method is called. In the `BreakablePlayer.cs` script:<br/>
_Note:_ the Physics force is applied individually on each one of the childs ("breakable" pieces).

```csharp
void Start() {
    _explosionDirections = new Vector3[] { Vector3.up, Vector3.right, -Vector3.one };

    // get all the childs ("breakable" pieces) and apply the Physics force individually on each one
    for (int i = 0; i < transform.childCount - 1; i++) {
        ImpulseByExplosion(transform.GetChild(i), i);
    }
}
```

Step 3 - let's give a little push to each "breakable" piece:<br/>
_Note:_ each child game object of the `Breakable Player` game object should have a `RigidBody` attached.

```csharp
void ImpulseByExplosion(Transform transform, int idx) {
    float factor = Random.Range(0.2f, 1.0f);
    float explosionImpulse = Random.Range(50.0f, 100.0f);
    transform.GetComponent<Rigidbody>().AddRelativeForce(_explosionDirections[idx % _explosionDirections.Length] * factor * explosionImpulse, ForceMode.Impulse);
}
```

#### Scripts:
[Player.cs](./Player.cs), [BreakablePlayer.cs](./BreakablePlayer.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Boost the Rocket](https://simmer.io/@alissin/boost-the-rocket).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!