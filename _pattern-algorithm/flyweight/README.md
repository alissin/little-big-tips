## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > Pattern / Algorithm

### flyweight pattern

Based on this playable demonstration / prototype: [The Dungeon](https://simmer.io/@alissin/the-dungeon).<br/>
Feel free to try the behaviour of this _**Little Big Tip**_.

_Note_: The purpose of this demonstration is to evaluate this gameplay mechanic. The FPS shooter gameplay mechanic itself, the amazing scenario and the props are free assets from the Asset Store.

> ![The Dungeon](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/the-dungeon.png)

#### Scenario
Imagine a lot of items like handgun mags, grenades, health potion scattered around the scene.

#### Problem description
In general, all of them have the same state (fields, values). For example, in our case: all health potions have the same amount of value to heal (`float 10.0f`), all handgun mags have the same amount of bullets (`int 9`).

Do we really need to repeat this information (`float 10.0f` and `int 9`) in any health potion or handgun mag game objects in the scene? Of course not, because this is a waste of memory!

Imagine a big game scenario where we could have hundred of instances of enemies repeating their state (fields, values). No way!

#### Solution simplified concept
With the _flyweight_ pattern, we are able to reuse a "container" object that will keep the information instead of repeat it in each game object that it is related.

#### Solution suggestion
In Unity, a very common and useful technique is to use `Scriptable Objects`.

In the hierarchy, create 2 game objects and name them as `Health Potion` and `Grenade`:

```
Hierarchy:
- Health Potion
- Grenade
```

Create a C# script `Item.cs` and attach this script to the `Health Potion` and `Grenade` game objects:<br/>

```csharp
public class Item : MonoBehaviour {
    ...
```

Create a C# script `ItemSO.cs` and extend `ScriptableObject`:<br/>
_Note_: Don't forget to remove the `Start()` and `Update()` methods.

```csharp
[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]
public class ItemSO : ScriptableObject {
    ...
```

With the attribute `CreateAssetMenu`, we will be able to create an item directly from the menu: in the project folder, click with the mouse right button and instead clicking on "C# Script", we will see a new menu option "Item" which will create a new file called "ItemSO".

But first, let's implement our `Scriptable Object`.

Step 1 - in the `ItemSO.cs` script, define the fields:

```csharp
public enum Type {
    MagHandgun, Grenade, HealthPotion
}

public Type type;
public float value;
public string description;
```

As you can see, now we can create the "containers" of each item type and later attach it to each game object.

Step 2 - let's create them: in Unity, in the project folder, find the `ItemSO.cs` script and click with the mouse right button on it. Click on "Create" and then "Item". Name it as `Health Potion`.

Step 3 - do the process again. Now, name the new file as `Grenade`.

Nice. Now we have our `ItemSO.cs` and 2 more files ("containers"): `Health Potion` and `Grenade`.

Step 4 - now, in the inspector, play with the values of these new 2 files.

Cool! We have predefined the settings of our items. We just need to use them.

Step 5 - in the `Item.cs` script, define the field:

```csharp
[SerializeField]
ItemSO _itemSO;
```

Step 6 - find the `Health Potion` and `Grenade` game objects in the hierarchy and in the inspector, set the corresponding `ItemSO` created on step 2 and 3.

Now from our `Item.cs` script, we can access all the information inside our `Scriptable Object`. 

Step 7 - let's try it and see it working:

```csharp
void Start() {
    Debug.Log(_itemSO.description + " " + _itemSO.value); // TODO: remove
}
```

Cool, we are done! Now, you can prefab the `Health Potion` and `Grenade` game objects, spread new instances of them around the scene and you can see that each one has the same predefined `ItemSO` attached. In memory, Unity keeps a unique instance of these individual "containers" and reuse them in each game object.

That's why the _flyweight_ pattern worths a lot!

As you can see only the common information to any object make sense to put inside the `ItemSO` class.

The local state, I mean, the specific fields you need to put them inside the `Item` class. For example, let's suppose that each `Item` has a random color:

```csharp
public class Item : MonoBehaviour {

    [SerializeField]
    ItemSO _itemSO;

    Color _randomColor; // TODO: set a random color on Start() method for example

    ...
```

#### Scripts:
[Item.cs](./Item.cs), [ItemSO.cs](./ItemSO.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [The Dungeon](https://simmer.io/@alissin/the-dungeon).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!