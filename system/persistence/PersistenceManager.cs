using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
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

    private const string PERSISTENCE_FILE_NAME = "/PersistenceData.sav";

    private List<IPersistence> persistenceList;

    private void Awake()
    {
        Instance = this;

        persistenceList = new List<IPersistence>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SaveData();
        else if (Input.GetKeyDown(KeyCode.L)) LoadData();
    }

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

    public void Register(IPersistence persistence)
    {
        persistenceList.Add(persistence);
    }

    public void Unregister(IPersistence persistence)
    {
        persistenceList.Remove(persistence);
    }
}