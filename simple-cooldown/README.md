## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > General tips

### simple cooldown

Based on this playable demonstration / prototype: [Realm Defender](https://simmer.io/@alissin/realm-defender).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

> ![Realm Defender](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/realm-defender.png)

#### Scenario
We can use only 3 weapons (catapults in this case) at each time. But, a cooldown system to reload theses weapons could bring a nice challenge to the player.

#### Solution suggestion
In this case, it was used a simple `UI Slider` to show this effect on the screen. You can use whatever you want.<br/>
_Note_: the scope of this _**Little Big Tip**_ is only the cooldown implementation.

In the hierarchy, create an `UI Slider` game object:<br/>
_Note_: an `UI Canvas` and an `EventSystem` game objects will be created automatically. Leave them there.

```
Hierarchy:
- Canvas
-- Slider
- EventSystem
```

Place the `Slider` wherever you want on the screen. Change the `Max Value` to 10 via inspector.

Create a C# script `Cooldown.cs` and attach this script to the `Slider` game object:

```csharp
public class Cooldown : MonoBehaviour {
    ...
```

Define the fields:

```csharp
[SerializeField]
float _cooldown = 5.0f;

Slider _cooldownSlider;

int _availableItems = 3;
int _maxItems = 3;
bool _isCooldownOn = false;
```

Don't forget to use the `UnityEngine.UI` namespace:

```csharp
using UnityEngine.UI;
```

Step 1 - get the `Slider` component:

```csharp
void Start() {
    _cooldownSlider = GetComponent<Slider>();
}
```

Step 2 - as you can see, we have 3 items set on `_maxItems` field and the cooldown system will run until all of them be reloaded. `_cooldown` field is set to 5.0f, that is, 5 seconds to reload each item:

```csharp
void Update() {
    if (_isCooldownOn) {
        _cooldownSlider.value += Time.deltaTime / _cooldown;

        if (_cooldownSlider.value >= _cooldownSlider.maxValue) {
            _availableItems++;
            _cooldownSlider.value = 0.0f;
        }

        _isCooldownOn = _availableItems < _maxItems;
    }
}
```

Step 3 - now, let's simulate when the player uses the items:<br/>
_Note:_ this will use the 3 items at the same time, just to see the cooldown in action.

```csharp
void Update() {
    ...

    if (Input.GetKeyDown(KeyCode.Space)) {
        _availableItems = 0;
        _isCooldownOn = true;
    }
}
```

#### Scripts:
[Cooldown.cs](./Cooldown.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Realm Defender](https://simmer.io/@alissin/realm-defender).

More _**Little Big Tips**_? Nice, [follow me](https://github.com/alissin/little-big-tips)!