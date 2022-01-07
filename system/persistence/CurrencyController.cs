using UnityEngine;

public class CurrencyController : MonoBehaviour, IPersistence
{
    private int currentSoftCurrency;
    private int currentHardCurrency;

    private void Awake()
    {
        PersistenceManager.Instance.Register(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentSoftCurrency++;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) currentHardCurrency++;
    }

    private void OnDestroy()
    {
        PersistenceManager.Instance.Unregister(this);
    }

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
}