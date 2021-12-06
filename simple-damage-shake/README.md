## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > General tips > simple damage shake

Feel free to try this behaviour on the playable demonstration / prototype: [Combat Wings](https://simmer.io/@alissin/combat-wings).

_Note_: The purpose of this demonstration is to evaluate this gameplay mechanic. The scenario and the props are free assets from the Asset Store.

> ![Combat Wings](./../z_images/combat_wings/combat-wings.png)

#### Problem description
When the player airplane is hit, the big impact needs to shake it all!

#### Solution suggestion
Actually, in this case, the `Camera` is shaked when something hits the `Player`.<br/>
_Note_: The scope of this _**Little Big Tip**_ is only the shakking. How to trigger the shaking after the `Player` is hit, it's up to you.

Create a C# script `Shake.cs` and attach this script to the `Camera` game object:

```csharp
public class Shake : MonoBehaviour
{
    ...
```

Define the fields:

```csharp
[SerializeField]
[Range(0.1f, 0.5f)]
float range = 0.2f;

[SerializeField]
bool isShaking = false;

Vector3 lastPos;
bool hasLastPosHeld = false;
```

We will use the `range` field to control the "bounce" of the shaking and the `lastPos` to keep the last position, before the shaking.

Let's make our `Camera` to shake a little bit:

```csharp
void Update()
{
    if (isShaking)
    {
        if (!hasLastPosHeld)
        {
            lastPos = transform.position;
            hasLastPosHeld = true;
        }

        float xPos = lastPos.x + Random.Range(-range, range);
        float yPos = lastPos.y + Random.Range(-range, range);
        float zPos = lastPos.z + Random.Range(-range, range);

        transform.position = new Vector3(xPos, yPos, zPos);
    }
    else
    {
        if (hasLastPosHeld)
        {
            transform.position = lastPos;
            hasLastPosHeld = false;
        }
    }
}
```

To simulate the shaking, hit play and change the `Is Shaking` via inspector to `true`. Cool, everything is shaking!<br/>
_Note:_ Put something in the scene, in front to the `Camera` to have a better look of the effect.

#### Scripts:
[Shake.cs](./Shake.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Combat Wings](https://simmer.io/@alissin/combat-wings).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!