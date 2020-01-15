## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > General tips

### airplane movement

Based on this playable demonstration / prototype: [Combat Wings](https://simmer.io/@alissin/combat-wings).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

_Note_: The purpose of this demonstration is to evaluate this gameplay mechanic. The amazing scenario and the props are free assets from the Asset Store.

> ![Combat Wings](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/combat-wings.png)

#### Scenario
Take control of the player airplane movement and face the enemies bravely!

#### Problem description
In this case, although the path rail mechanism is responsible to move the airplane through the scenario, it's up to us to take care of the airplane local movement.

#### Solution simplified concept
The player airplane local movement implementation is based on the [Aircraft principal axes](https://en.wikipedia.org/wiki/Aircraft_principal_axes) concept. So, the Pitch factor will be applied to the X axis, Yaw factor to the Y axis and Roll factor to the Z axis.

#### Solution suggestion
In this case, the path rail mechanism actually controls the `Camera` game object and the `Player` game object is nested (as a child) to it:

```
Hierarchy:
- Camera
-- Player
```

Create a C# script `Player.cs` and attach this script to the `Player` game object:

```csharp
public class Player : MonoBehaviour {
    ...
```

Define the fields:

```csharp
[SerializeField]
[Range(5.0f, 20.0f)]
float _movementSpeed = 8.0f;

[SerializeField]
float _yawFactor = 3.0f;

[SerializeField]
float _pitchFactor = 2.5f;

[SerializeField]
float _rotationFactor = 20.0f;

[SerializeField]
float _rollFactor = 15.0f;

const float _maxHorRangePosition = 5.0f;
const float _maxVerRangePosition = 4.0f;

float _lh, _lv;
```

Step 1 - get the axis (movement) values to setup the position and rotation on every frame:

```csharp
void Update() {
    _lh = Input.GetAxis("Horizontal");
    _lv = Input.GetAxis("Vertical");

    SetupPosition();
    SetupRotation();
}
```

Step 2 - let's setup the position:<br/>
_Note_: we will use the localPosition because `Player` is a child of the Main Camera and the `Clamp` method to limit the screen.

```csharp
void SetupPosition() {
    // horizontal and vertical offset
    float realTimeMovementSpeed = _movementSpeed * Time.deltaTime;
    float lhOffset = _lh * realTimeMovementSpeed;
    float lvOffset = _lv * realTimeMovementSpeed;

    // clamp the values to limit the screen
    float xPos = Mathf.Clamp(lhOffset + transform.localPosition.x, -_maxHorRangePosition, _maxHorRangePosition);
    float yPos = Mathf.Clamp(-lvOffset + transform.localPosition.y, -_maxVerRangePosition, _maxVerRangePosition);

    // localPosition because `Player` is a child of the Main Camera
    transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
}
```

Step 3 - finally, let's setup the rotation:<br/>
_Note_: we will use the localRotation because `Player` is a child of the Main Camera.

```csharp
void SetupRotation() {
    float xRot = transform.localPosition.y * -_pitchFactor + _lv * _rotationFactor;
    float yRot = transform.localPosition.x * _yawFactor + _lh * _rotationFactor;
    float zRot = -_lh * _rollFactor;

    transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
}
```

#### Scripts:
[Player.cs](./Player.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Combat Wings](https://simmer.io/@alissin/combat-wings).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!