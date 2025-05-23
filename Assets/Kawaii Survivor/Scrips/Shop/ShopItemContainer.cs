using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItemContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header("Stats")]
    [SerializeField] private Transform statContainersParent;

    [SerializeField] public Button purchaseButton;

    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outLine;

    [Header("Lock Elements")]
    [SerializeField] private Image lockImage;
    [SerializeField] private Sprite lockedSpike, unlockedSpike;
    public bool IsLocked { get; private set; }

    [field: Header("Purchasing")]
    public WeaponDataSO WeaponData { get; private set; }
    public ObjectDataSO ObjectData { get; private set; }
    private int weaponLevel;


    [Header("Actions")]
    public static Action<ShopItemContainer, int> onPurchased;

    private void Awake()
    {
        CurrencyManager.onUpdated += CurrencyUpdateCallback;
    }

    private void CurrencyUpdateCallback()
    {
        int itemPrice;

        if (WeaponData != null)
            itemPrice = WeaponStatsCalculator.GetPurchasePrice(WeaponData, weaponLevel);
        else
            itemPrice = ObjectData.Price;

        purchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(itemPrice);
    }

    private void OnDestroy()
    {
        CurrencyManager.onUpdated -= CurrencyUpdateCallback;
    }

    public void Configure(WeaponDataSO weaponData, int level)
    {
        WeaponData = weaponData;

        weaponLevel = level;

        icon.sprite = weaponData.Sprite;
        nameText.text = weaponData.Name + $" (lvl {level + 1} )";

        int weaponPrice = WeaponStatsCalculator.GetPurchasePrice(weaponData, level);
        priceText.text = weaponPrice.ToString();


        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;

        outLine.color = ColorHolder.GetOutlineColor(level);

        foreach (Image image in levelDependentImages)
            image.color = imageColor;
        Dictionary<Stat, float> calculatorStats = WeaponStatsCalculator.GetStats(weaponData, level);
        ConfigureStatContiners(calculatorStats);

        purchaseButton.onClick.AddListener(Purchase);
        purchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(weaponPrice);
    }

    public void Configure(ObjectDataSO objectData)
    {
        ObjectData = objectData;

        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name;
        priceText.text = objectData.Price.ToString();

        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;

        outLine.color = ColorHolder.GetOutlineColor(objectData.Rarity);

        foreach (Image image in levelDependentImages)
            image.color = imageColor;
        ConfigureStatContiners(objectData.BaseStats);


        purchaseButton.onClick.AddListener(Purchase);
        purchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(objectData.Price);
    }

    private void ConfigureStatContiners(Dictionary<Stat, float> stats)
    {
        statContainersParent.Clear();
        StatContainerManager.GenerateStatContainers(stats, statContainersParent);
    }

    private void Purchase()
    {
        onPurchased?.Invoke(this, weaponLevel);
    }

    public void LockButtonCallback()
    {
        IsLocked = !IsLocked;
        UpdateLockVisuals();
    }

    private void UpdateLockVisuals()
    {
        lockImage.sprite = IsLocked ? lockedSpike : unlockedSpike;
    }


}
