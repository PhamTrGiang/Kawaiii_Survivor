using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestObjectContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Stats")]
    [SerializeField] private Transform statContainersParent;

    [field: SerializeField] public Button TakeButton { get; private set; }
    [field: SerializeField] public Button RecycleButton { get; private set; }

    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outLine;

    public void Configure(ObjectDataSO objectData)
    {
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name;

        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;

        outLine.color = ColorHolder.GetOutlineColor(objectData.Rarity);

        foreach (Image image in levelDependentImages)
            image.color = imageColor;
        ConfigureStatContiners(objectData.BaseStats);
    }

    private void ConfigureStatContiners(Dictionary<Stat, float> stats)
    {
        StatContainerManager.GenerateStatContainers(stats, statContainersParent);
    }
}
