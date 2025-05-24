using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [Header("Player Stats Elements")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private RectTransform playerStatsClosePanel;
    private Vector2 playerStatsOpenedPos, playerStatsClosePos;

    [Header("Inventory Elements")]
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private RectTransform inventoryClosePanel;
    private Vector2 inventoryOpenedPos, inventoryClosePos;

    [Header("Item Info Elements")]
    [SerializeField] private RectTransform itemInfoSlidePanel;
    private Vector2 itemInfoOpenedPos, itemInfoClosePos;




    IEnumerator Start()
    {
        yield return null;

        ConfiguraPlayerStatsPanel();
        ConfiguraInventoryPanel();
        ConfiguraItemInfoPanel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ConfiguraPlayerStatsPanel()
    {
        float width = Screen.width / (4 * playerStatsPanel.lossyScale.x);
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x: width);

        playerStatsOpenedPos = playerStatsPanel.anchoredPosition;
        playerStatsClosePos = playerStatsOpenedPos + Vector2.left * width;

        playerStatsPanel.anchoredPosition = playerStatsClosePos;

        HidePlayerStats();
    }

    public void ShowPlayerStats()
    {
        playerStatsPanel.gameObject.SetActive(true);
        playerStatsClosePanel.gameObject.SetActive(true);
        playerStatsClosePanel.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsOpenedPos, .5f).setEase(LeanTweenType.easeInCubic);

        LeanTween.cancel(playerStatsClosePanel);
        LeanTween.alpha(playerStatsClosePanel, .8f, .5f).setRecursive(false);

    }
    public void HidePlayerStats()
    {
        playerStatsClosePanel.GetComponent<Image>().raycastTarget = false;

        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsClosePos, .5f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => playerStatsPanel.gameObject.SetActive(false));


        LeanTween.cancel(playerStatsClosePanel);
        LeanTween.alpha(playerStatsClosePanel, 0, .5f).setRecursive(false)
            .setOnComplete(() => playerStatsClosePanel.gameObject.SetActive(false));
    }


    private void ConfiguraInventoryPanel()
    {
        float width = Screen.width / (4 * inventoryPanel.lossyScale.x);
        inventoryPanel.offsetMin = inventoryPanel.offsetMin.With(x: -width);

        inventoryOpenedPos = inventoryPanel.anchoredPosition;
        inventoryClosePos = inventoryOpenedPos - Vector2.left * width;

        inventoryPanel.anchoredPosition = inventoryClosePos;

        HideInventory(false);
    }

    public void ShowInventory()
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryClosePanel.gameObject.SetActive(true);
        inventoryClosePanel.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, inventoryOpenedPos, .5f).setEase(LeanTweenType.easeInCubic);

        LeanTween.cancel(inventoryClosePanel);
        LeanTween.alpha(inventoryClosePanel, .8f, .5f).setRecursive(false);

    }
    public void HideInventory(bool hideItemInfo = true)
    {
        inventoryClosePanel.GetComponent<Image>().raycastTarget = false;

        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, inventoryClosePos, .5f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => inventoryPanel.gameObject.SetActive(false));


        LeanTween.cancel(inventoryClosePanel);
        LeanTween.alpha(inventoryClosePanel, 0, .5f).setRecursive(false)
            .setOnComplete(() => inventoryClosePanel.gameObject.SetActive(false));

        if (hideItemInfo)
            HideItemInfo();
    }

    private void ConfiguraItemInfoPanel()
    {
        float height = Screen.height / (2 * itemInfoSlidePanel.lossyScale.x);
        itemInfoSlidePanel.offsetMax = itemInfoSlidePanel.offsetMax.With(y: height);

        itemInfoOpenedPos = itemInfoSlidePanel.anchoredPosition;
        itemInfoClosePos = itemInfoOpenedPos + Vector2.down * height;

        itemInfoSlidePanel.anchoredPosition = itemInfoClosePos;

        itemInfoSlidePanel.gameObject.SetActive(false);
    }

    [NaughtyAttributes.Button]
    public void ShowItemInfo()
    {
        itemInfoSlidePanel.gameObject.SetActive(true);

        itemInfoSlidePanel.LeanCancel();
        itemInfoSlidePanel.LeanMove((Vector3)itemInfoOpenedPos, .3f)
            .setEase(LeanTweenType.easeOutCubic);
    }

    [NaughtyAttributes.Button]
    public void HideItemInfo()
    {
        itemInfoSlidePanel.LeanCancel();
        itemInfoSlidePanel.LeanMove((Vector3)itemInfoClosePos, .3f)
            .setEase(LeanTweenType.easeInCubic)
            .setOnComplete(() => itemInfoSlidePanel.gameObject.SetActive(false));

    }
}
