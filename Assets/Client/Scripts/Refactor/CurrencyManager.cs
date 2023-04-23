using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] List<Currency> currencyList;

    public void IncreaseCurrency(int currencyId, int value)
    {
        foreach (Currency currency in currencyList)
        {
            if (currency.id == currencyId)
            {
                currency.Increase(value);
                Debug.Log(currency.name + " has been increased.");
            }
        }
    }

    public void DecreaseCurrency(int currencyId, int value)
    {
        foreach (Currency currency in currencyList)
        {
            if (currency.id == currencyId)
            {
                currency.Decrease(value);
                Debug.Log(currency.name + " has been decreased.");
            }
        }
    }
}

[System.Serializable]
public class Currency
{
    public int id;
    public string name;
    public string description;

    private int _currencyCount;

    public void Increase(int value) { _currencyCount += value; }
    public void Decrease(int value) { _currencyCount -= value; }
    public int GetCount() { return _currencyCount; }

    Currency()
    {
        id = 0;
        name = "Coin";
        description = "Required to purchase in-game items...";
        _currencyCount = 0;
    }
}