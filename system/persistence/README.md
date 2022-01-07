## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > Systems > persistence system

#### Problem description
How deal with saving and loading so many information distributed in the code?

#### Solution suggestion
We will implement a persistence system that gets each piece of data in its specific place in the code and saves it to a file. We will then read this file and put each piece of data back in its proper place during loading.<br/>
<sub>_Note:_ To keep this example simple, we will only persist the player's last position, direction and the currency values (soft and hard currencies). After that, you will get the idea and will be able to use the persistence system to deal with other data with no problem.</sub>

Firstly, let's create a class that will represent our persistence data. Create a C# script `PersistenceData.cs`:<br/>
<sub>_Note:_ It's a file that holds some `serializable` classes (not `MonoBehaviour`) inside of it.</sub>

```csharp
using System;
using UnityEngine;

[Serializable]
public class PersistenceData
{
    [SerializeField]
    private PlayerData playerData;
    public PlayerData PlayerData
    {
        get => playerData;
        set => playerData = value;
    }

    [SerializeField]
    private CurrencyData currencyData;
    public CurrencyData CurrencyData
    {
        get => currencyData;
        set => currencyData = value;
    }
}

[Serializable]
public class PlayerData
{
    [SerializeField]
    private float[] lastPosition;
    public float[] LastPosition => lastPosition;

    [SerializeField]
    private float[] lastDirection;
    public float[] LastDirection => lastDirection;

    public PlayerData(float[] lastPosition, float[] lastDirection)
    {
        this.lastPosition = lastPosition;
        this.lastDirection = lastDirection;
    }
}

[Serializable]
public class CurrencyData
{
    [SerializeField]
    private int currentSoftCurrency;
    public int CurrentSoftCurrency => currentSoftCurrency;

    [SerializeField]
    private int currentHardCurrency;
    public int CurrentHardCurrency => currentHardCurrency;

    public CurrencyData(int currentSoftCurrency, int currentHardCurrency)
    {
        this.currentSoftCurrency = currentSoftCurrency;
        this.currentHardCurrency = currentHardCurrency;
    }
}
```

As you can see, the `PlayerData` is responsible for the player's last position and direction and the `CurrencyData` is responsible for the currency values (soft and hard currencies).

Now, let's create our interface. Every component that has data which should be persisted, will implement it. Create a C# script `IPersistence.cs`:

```csharp
public interface IPersistence
{
    void OnSave(PersistenceData persistenceData);
    void OnLoad(PersistenceData persistenceData);
}
```

Since we have all we need, let's create our components. In the hierarchy, create 2 game objects and 1 capsule and name them as `Persistence Manager`, `Currency Controller` and `Player`:

```
Hierarchy:
- Persistence Manager
- Currency Controller
- Player
```

Create a C# script `PersistenceManager.cs` and attach this script to the `Persistence Manager` game object:

```csharp
public class PersistenceManager : MonoBehaviour
{
    ...
```

Make the `PersistenceManager.cs` run as a singleton:<br/>
<sub>_Note:_ In this case, I'm using the [singleton pattern](../singleton) to easily access the `Persistence System`.</sub>

```csharp
private static PersistenceManager instance;

public static PersistenceManager Instance
{
    get
    {
        return instance;
    }
    set
    {
        if (instance == null)
        {
            instance = value;
            DontDestroyOnLoad(instance.gameObject);
        }
    }
}
```

Define the fields:

```csharp
private const string PERSISTENCE_FILE_NAME = "/PersistenceData.sav";

private List<IPersistence> persistenceList;
```

Let's implement the initialization:

```csharp
private void Awake()
{
    Instance = this;

    persistenceList = new List<IPersistence>();
}
```

Now, the save flow:

```csharp
private void SaveData()
{
    FinishSaveData(GeneratePersistenceData());
}

private PersistenceData GeneratePersistenceData()
{
    PersistenceData persistenceData = new PersistenceData();
    foreach (var item in persistenceList)
    {
        item.OnSave(persistenceData);
    }
    return persistenceData;
}

private void FinishSaveData(PersistenceData persistenceData)
{
    string filePath = Application.persistentDataPath + PERSISTENCE_FILE_NAME;
    FileStream fileStream = new FileStream(filePath, FileMode.Create);
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    binaryFormatter.Serialize(fileStream, persistenceData);
    fileStream.Close();

    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Saved Data - ");
    stringBuilder.Append($"Player's last position: ({persistenceData.PlayerData.LastPosition[0]}, {persistenceData.PlayerData.LastPosition[1]}, {persistenceData.PlayerData.LastPosition[2]}) ")
        .Append($" | Currency SC / HC: {persistenceData.CurrencyData.CurrentSoftCurrency} / {persistenceData.CurrencyData.CurrentHardCurrency}");
    stringBuilder.Append($"\nFile : {filePath}");
    Debug.Log(stringBuilder);
}
```

The load flow:

```csharp
public void LoadData()
{
    PersistenceData persistenceData = GetDeserializedPersistenceData();
    if (persistenceData == null) return;

    FinishLoadData(persistenceData);
}

private PersistenceData GetDeserializedPersistenceData()
{
    PersistenceData persistenceData = null;
    string filePath = Application.persistentDataPath + PERSISTENCE_FILE_NAME;
    if (File.Exists(filePath))
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        persistenceData = binaryFormatter.Deserialize(fileStream) as PersistenceData;
        fileStream.Close();
    }
    return persistenceData;
}

private void FinishLoadData(PersistenceData persistenceData)
{
    foreach (var item in persistenceList)
    {
        item.OnLoad(persistenceData);
    }

    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Loaded Data - ");
    stringBuilder.Append($"Player's last position: ({persistenceData.PlayerData.LastPosition[0]}, {persistenceData.PlayerData.LastPosition[1]}, {persistenceData.PlayerData.LastPosition[2]}) ")
        .Append($" | Currency SC / HC: {persistenceData.CurrencyData.CurrentSoftCurrency} / {persistenceData.CurrencyData.CurrentHardCurrency}");
    Debug.Log(stringBuilder);
}
```

As you can se, the trick here is to keep all components that should have its data saved or loaded in this `persistenceList`. Then, call the `OnSave()` and `OnLoad()` methods like so:

```csharp
public class PersistenceManager : MonoBehaviour
{
    ...

    // save flow
    private PersistenceData GeneratePersistenceData()
    {
        PersistenceData persistenceData = new PersistenceData();
        foreach (var item in persistenceList)
        {
            item.OnSave(persistenceData);
        }
    
    ...
        
    // load flow
    private void FinishLoadData(PersistenceData persistenceData)
    {
        foreach (var item in persistenceList)
        {
            item.OnLoad(persistenceData);
        }

    ...
```

 To make this happen, as discussed earlier, these components must implement our interface `IPersistence.cs` (we will do it soon). But how to put these components in our list inside the `PersistenceManager.cs`? We just need to allow the components to register and unregister themselves on the persistence system. We can easily implement it like so:

```csharp
public void Register(IPersistence persistence)
{
    persistenceList.Add(persistence);
}

public void Unregister(IPersistence persistence)
{
    persistenceList.Remove(persistence);
}
```

Just to simulate the save and load features, let's implement this simple input logic:

```csharp
private void Update()
{
    if (Input.GetKeyDown(KeyCode.S)) SaveData();
    else if (Input.GetKeyDown(KeyCode.L)) LoadData();
}
```

Now that we have our persistence system implemented, let's make use of it!

Create a C# script `CurrencyController.cs` that implements our interface `IPersistence.cs` and attach this script to the `Currency Controller` game object:

```csharp
public class CurrencyController : MonoBehaviour, IPersistence
{
    ...
```

Define the fields:

```csharp
private int currentSoftCurrency;
private int currentHardCurrency;
```

Let's make this script to use the persistence system. We need to register and unregister on it:

```csharp
private void Awake()
{
    PersistenceManager.Instance.Register(this);
}

private void OnDestroy()
{
    PersistenceManager.Instance.Unregister(this);
}
```

Now, the save and load logic for the currency values:

```csharp
public void OnSave(PersistenceData persistenceData)
{
    persistenceData.CurrencyData = new CurrencyData(currentSoftCurrency, currentHardCurrency);
}

public void OnLoad(PersistenceData persistenceData)
{
    CurrencyData currencyData = persistenceData.CurrencyData;
    currentSoftCurrency = currencyData.CurrentSoftCurrency;
    currentHardCurrency = currencyData.CurrentHardCurrency;
}
```

Just to simulate an increase of the SC or HC values, let's implement this simple input logic:

```csharp
private void Update()
{
    if (Input.GetKeyDown(KeyCode.Alpha1)) currentSoftCurrency++;
    else if (Input.GetKeyDown(KeyCode.Alpha2)) currentHardCurrency++;
}
```

We are almost there! Now, let's deal with the player. Create a C# script `Player.cs` that implements our interface `IPersistence.cs` and attach this script to the `Player` game object:

```csharp
public class Player : MonoBehaviour, IPersistence
{
    ...
```

Let's make this script to use the persistence system. We need to register and unregister on it:

```csharp
private void Awake()
{
    PersistenceManager.Instance.Register(this);
}

private void OnDestroy()
{
    PersistenceManager.Instance.Unregister(this);
}
```

Now, the save and load logic for player's last position and direction:

```csharp
public void OnSave(PersistenceData persistenceData)
{
    Vector3 localPosition = transform.localPosition;
    float[] lastPosition = { localPosition.x, localPosition.y, localPosition.z };
    Vector3 forward = transform.forward;
    float[] lastDirection = { forward.x, forward.y, forward.z };
    persistenceData.PlayerData = new PlayerData(lastPosition, lastDirection);
}

public void OnLoad(PersistenceData persistenceData)
{
    PlayerData playerData = persistenceData.PlayerData;
    transform.localPosition = new Vector3(playerData.LastPosition[0], playerData.LastPosition[1], playerData.LastPosition[2]);
    transform.forward = new Vector3(playerData.LastDirection[0], playerData.LastDirection[1], playerData.LastDirection[2]);
}
```

Just to simulate the player's movement, let's implement this simple input logic:

```csharp
private void Update()
{
    if (Input.GetKeyDown(KeyCode.RightArrow)) transform.localPosition += Vector3.right;
    else if (Input.GetKeyDown(KeyCode.LeftArrow)) transform.localPosition += Vector3.left;
}
```

That's it! Now, let's try our persistence system! Play the game and use the keyboard keys:
- right arrow to move the player to the right;
- left arrow to move the player to the left;
- (number) 1 to increase the SC value;
- (number) 2 to increase the HC value;

When you finish, hit `S` to save the game. You should see a console message like this:<br/>
<sub>_Note:_ You can see the place where the file was saved as well.</sub>

> ![save log](./save-log.png)

Exit the play mode, run the game again and hit `L` to load the game. You should see a console message like this, have the `Player`'s game object automatically moved and the currency values changed as well:

> ![load log](./load-log.png)

You can make it even better allowing the persistence system to save and load the data automatically. Just implement this in the `PersistenceManager.cs`:

```csharp
private void OnApplicationPause(bool pauseStatus)
{
    // if the user "kills" the app
    if (pauseStatus)
    {
        SaveData();
    }
    else
    {
        LoadData();
    }
}

private void OnApplicationQuit()
{
    // escape on editor / back on android app
    SaveData();
}
```

#### Scripts:
[PersistenceManager.cs](./PersistenceManager.cs), [PersistenceData.cs](./PersistenceData.cs), [IPersistence.cs](./IPersistence.cs), [CurrencyController.cs](./CurrencyController.cs), [Player.cs](./Player.cs)

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!