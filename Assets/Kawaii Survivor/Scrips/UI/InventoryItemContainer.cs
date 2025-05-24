using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemContainer : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Image container;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    public int Index { get; private set; }

    public Weapon Weapon { get; private set; }
    public ObjectDataSO ObjectData { get; private set; }

    public void Configure(Color containerColor, Sprite itemIcon)
    {
        container.color = containerColor;
        icon.sprite = itemIcon;
    }
    public void Configure(Weapon weapon, int index, Action clickCallback)
    {
        Weapon = weapon;
        Index = index;

        Color color = ColorHolder.GetColor(weapon.Level);
        Sprite icon = weapon.WeaponData.Sprite;

        Configure(color, icon);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => clickCallback?.Invoke());
    }
    public void Configure(ObjectDataSO objectData, Action clickCallback)
    {
        ObjectData = objectData;

        Color color = ColorHolder.GetColor(objectData.Rarity);
        Sprite icon = objectData.Icon;

        Configure(color, icon);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => clickCallback?.Invoke());
    }

}
