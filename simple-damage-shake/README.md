## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > General tips

### simple damage shake

Based on this playable demonstration / prototype: [Combat Wings](https://simmer.io/@alissin/combat-wings).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

_Note_: The purpose of this demonstration is to evaluate this gameplay mechanic. The amazing scenario and the props are free assets from the Asset Store.

> ![Combat Wings](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/combat-wings.png)

#### Scenario
When the player airplane is hit, the big impact will shake it all!

#### Solution suggestion
Actually, in this case, the `Camera` is shaked when something hits the `Player`.<br/>
_Note_: The scope of this _**Little Big Tip**_ is only the shakking. How to start the shaking after the `Player` be hit, it's up to you.

Create a C# script `Shake.cs` and attach this script to the `Camera` game object:

```csharp
public class Shake : MonoBehaviour {
    ...
```

Define the fields:

```csharp
[SerializeField]
[Range(0.1f, 0.5f)]
float _range = 0.2f;

[SerializeField]
bool _isShaking = false;

Vector3 _lastPos;
bool _hasLastPosHeld = false;
```

We will use the `_range` field to control the "bounce" of the shaking and the `_lastPos` to keep the last position, before the shaking.

Step 1 - let's make our `Camera` shake a little bit:

```csharp
void Update() {
    if (_isShaking) {
        if (!_hasLastPosHeld) {
            _lastPos = transform.position;
            _hasLastPosHeld = true;
        }

        float xPos = _lastPos.x + Random.Range(-_range, _range);
        float yPos = _lastPos.y + Random.Range(-_range, _range);
        float zPos = _lastPos.z + Random.Range(-_range, _range);

        transform.position = new Vector3(xPos, yPos, zPos);
    } else {
        if (_hasLastPosHeld) {
            transform.position = _lastPos;
            _hasLastPosHeld = false;
        }
    }
}
```

Step 2 - to simulate the shaking, hit play and change the `Is Shaking` via inspector to `true`. Cool, everything is shaking!<br/>
_Note:_ Put something in the scene, in front to the `Camera` to have a better look of the effect.

#### Scripts:
[Shake.cs](./Shake.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Combat Wings](https://simmer.io/@alissin/combat-wings).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!