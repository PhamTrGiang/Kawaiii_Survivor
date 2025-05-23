using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    [field: SerializeField] public int Currency { get; private set; }


    [Header("Actions")]
    public static Action onUpdated;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateText();
    }
    [NaughtyAttributes.Button]
    private void Add500Currency()
    {
        AddCurrency(500);
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        UpdateText();

        onUpdated?.Invoke();
    }

    public void UseCurrency(int price) => AddCurrency(-price);

    private void UpdateText()
    {
        CurrencyText[] currencyTexts = FindObjectsByType<CurrencyText>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (CurrencyText text in currencyTexts)
            text.UpdateText(Currency.ToString());
    }

    public bool HasEnoughCurrency(int price)
    {
        return Currency >= price;
    }


}
