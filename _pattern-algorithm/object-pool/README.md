## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > Pattern / Algorithm

### object pool pattern

Based on this playable demonstration / prototype: [Realm Defender](https://simmer.io/@alissin/realm-defender).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

> ![Realm Defender](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/realm-defender.png)

#### Scenario
Strong enemy warriors are spawning from enemy base (red castle) and going to attack the player base (blue castle).

#### Problem description
You could use `Instantiate()` and `Destroy()` methods to spawn / destroy the enemy warriors but it is not good practice because it has a high cost in performance. Imagine a game with different waves of enemies for example.

#### Solution simplified concept
Instead, you can load (instantiate) a pool of objects at the beginning, recycle them and use them whenever you want, as many as you want.

#### Solution suggestion
In this case, in this level, an object pool with size of 5 objects was enough because this level could have a max of 4 warriors at a time walking on the path. Of course, in a level with a long path, you could increase the size of the object pool.

In the hierarchy, create a game object and name it as `Spawn Controller`:
_Note_: it will be like a container to all instantiated game objects (warriors) that will stay nested (as a child) to this game object.

```
Hierarchy:
- Spawn Controller
```

Create a C# script `SpawnController.cs` and attach this script to the `Spawn Controller` game object:

```csharp
public class SpawnController : MonoBehaviour {
    ...
```

Define the fields:

```csharp
[SerializeField]
GameObject _spawnPrefab;

[SerializeField]
float _spawnDelay;

[SerializeField]
int _poolSize;

GameObject[] _poolObjs;

int _currentPoolSize;
```

When your level is loaded, start the process.

Step 1 - init the object pool:

```csharp
void InitPool() {
    _currentPoolSize = _poolSize;
    _poolObjs = new GameObject[_currentPoolSize];

    for (int i = 0; i < _poolObjs.Length; i++) {
        GameObject itemClone = Instantiate(_spawnPrefab, transform);
        itemClone.SetActive(false);
        _poolObjs[i] = itemClone;
    }
}
```

Cool! At this time, we already have all game objects instantiated and ready to use.<br/>
You can use a spawn mechanism of your choice. In this case, a Coroutine will be used.

Step 2 - use the objects of the pool:

```csharp
IEnumerator SpawnRoutine(Vector3 spawnPos, Quaternion spawnRot) {
    while (true) {
        GetObjFromPool(spawnPos, spawnRot);
        yield return new WaitForSeconds(_spawnDelay);
    }
}

GameObject GetObjFromPool(Vector3 spawnPos, Quaternion spawnRot) {
    // if there is no more available items in the pool, do nothing
    if (_currentPoolSize == 0) {
        return null;
    }

    _currentPoolSize--;

    GameObject obj = _poolObjs[_currentPoolSize];
    _poolObjs[_currentPoolSize] = null;

    obj.transform.position = spawnPos;
    obj.transform.rotation = spawnRot;
    obj.SetActive(true);

    return obj;
}
```

Step 3 - once we are done with the object, we need to return it to the pool:

```csharp
public void ReturnObjToPool(GameObject obj) {
    obj.SetActive(false);

    _poolObjs[_currentPoolSize] = obj;
    _currentPoolSize++;
}
```

You can see here that you use the array to control which object is available and the `SetActive()` method to actually "use" the object. It is important in this case because there is no need to keep the object `active == true` when we are not using it.

Step 4 - create the `Enemy` game object on hierarchy, create a C# script `Enemy.cs` and attach this script to the `Enemy` game object and finally create the `Enemy Prefab`. Don't forget to set the field `_spawnPrefab` on `Spawn Controller` game object via inspector.

Step 5 - in the `Enemy.cs` script, call this `OnHide()` method when you do not need the object anymore and want to return it to the object pool. In this case, the warrior is hidden when it gets to the player base (blue castle):

```csharp
public void OnHide() {
    // reset the object default position
    transform.position = _defaultPos;

    // TODO: you will need a reference of the `Spawn Controller` script
    _spawnController.ReturnObjToPool(gameObject);
}
```

To keep this example simple and focused on the object pool mechanism, it's up to you to decide how to get the reference of `Spawn Controller` script. You could use `FindObjectOfType<SpawnController>()` but I suggest the observer pattern, callback mechanism like an `Event` / `Action` or even the [singleton pattern](../singleton) with a global access on this spawner mechanism.

#### Scripts:
[SpawnController.cs](./SpawnController.cs), [Enemy.cs](./Enemy.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Realm Defender](https://simmer.io/@alissin/realm-defender).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!