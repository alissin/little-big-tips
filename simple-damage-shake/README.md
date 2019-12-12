## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > General tips

### simple damage shake

Based on this playable demonstration / prototype: [Combat Wings](https://simmer.io/@alissin/combat-wings).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

> ![Combat Wings](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/combat-wings.png)

#### Scenario
When the player airplane is hit, the big impact will shake it all!

#### Solution suggestion
In this case, the path rail mechanism is responsible to move the airplane through the scenario. The path rail mechanism actually controls the `Camera` game object and the `Player` game object is nested (as a child) to it:

```
Hierarchy:
- Camera
-- Player
```

So, in this case, the `Camera` is shaked when something hits the `Player`.

Create a C# script `Shake.cs` and attach this script to the `Camera` game object:

```csharp
public class Shake : MonoBehaviour {
    ...
```

Define the fields:

```csharp
[SerializeField]
float _frequency = 1000.0f;

[SerializeField]
float _amplitude = 10.0f;

[SerializeField]
bool _isShaking = false;
```

We will use the `Mathf.Sin` function to control the "bounce" of the shake.

Step 1 - let's make our `Camera` shake a little bit:

```csharp
void Update() {
    if (_isShaking) {
        float factor = Mathf.Sin(Time.time * _frequency) * _amplitude;

        float xPos = transform.position.x + factor * Random.Range(-1, 2);
        float yPos = transform.position.y + factor * Random.Range(-1, 2);
        float zPos = transform.position.z + factor * Random.Range(-1, 2);

        transform.position = Vector3.Lerp(transform.position, new Vector3(xPos, yPos, zPos), Time.deltaTime);
    }
}
```

Step 2 - just to simulate the shaking, hit play and change the `Is Shaking` via inspector to `true`. Cool, everything is shaking!

#### Scripts:
[Shake.cs](./Shake.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Combat Wings](https://simmer.io/@alissin/combat-wings).

More _**Little Big Tips**_? Nice, [follow me](https://github.com/alissin/little-big-tips)!