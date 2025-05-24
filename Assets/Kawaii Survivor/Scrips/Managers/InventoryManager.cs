using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameStateListener
{
    [Header("Player Components")]
    [SerializeField] private PlayerWeapons playerWeapons;
    [SerializeField] private PlayerObjects playerObjects;

    [Header("Elements")]
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private InventoryItemContainer inventoryItemContainer;
    [SerializeField] private ShopManagerUI shopManagerUI;
    [SerializeField] private InventoryItemInfo itemInfo;

    private void Awake()
    {
        ShopManager.onItemPurchased += ItemPurchaseCallback;
        WeaponMerge.onMerge += WeaponMergeCallback;
    }

    private void OnDestroy()
    {
        ShopManager.onItemPurchased -= ItemPurchaseCallback;
        WeaponMerge.onMerge -= WeaponMergeCallback;

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStateChangedCallback(GameState gameState)
    {
        if (gameState == GameState.SHOP)
            Configure();
    }
    private void Configure()
    {
        inventoryItemsParent.Clear();

        Weapon[] weapons = playerWeapons.GetWeapons();
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
                continue;

            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);
            container.Configure(weapons[i], i, () => ShowItemInfo(container));
        }


        ObjectDataSO[] objectDatas = playerObjects.Objects.ToArray();
        for (int i = 0; i < objectDatas.Length; i++)
        {
            InventoryItemContainer container = Instantiate(inventoryItemContainer, inventoryItemsParent);
            container.Configure(objectDatas[i], () => ShowItemInfo(container));
        }
    }

    private void ShowItemInfo(InventoryItemContainer container)
    {
        if (container.Weapon != null)
            ShowWeaponInfo(container.Weapon, container.Index);
        else
            ShowObjectInfo(container.ObjectData);
    }

    private void ShowWeaponInfo(Weapon weapon, int index)
    {
        itemInfo.Configure(weapon);

        itemInfo.RecycleButton.onClick.RemoveAllListeners();
        itemInfo.RecycleButton.onClick.AddListener(() => RecycleWeapon(index));

        shopManagerUI.ShowItemInfo();
    }

    private void RecycleWeapon(int index)
    {
        playerWeapons.RecycleWeapon(index);

        Configure();

        shopManagerUI.HideItemInfo();
    }

    private void ShowObjectInfo(ObjectDataSO objectData)
    {
        itemInfo.Configure(objectData);

        itemInfo.RecycleButton.onClick.RemoveAllListeners();
        itemInfo.RecycleButton.onClick.AddListener(() => RecycleObject(objectData));

        shopManagerUI.ShowItemInfo();
    }

    private void RecycleObject(ObjectDataSO objectToRecycle)
    {
        playerObjects.RecycleObject(objectToRecycle);

        Configure();

        shopManagerUI.HideItemInfo();
    }

    private void ItemPurchaseCallback() => Configure();

    private void WeaponMergeCallback(Weapon mergeWeapon)
    {
        Configure();
        itemInfo.Configure(mergeWeapon);
    }
}