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