using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    const string statIconsDataPath = "Data/Stat Icons";

    private static StatIcon[] statIcons;
    public static Sprite GetStatIcon(Stat stat)
    {
        if (statIcons == null)
        {
            StatIconDataSO data = Resources.Load<StatIconDataSO>(statIconsDataPath);
            statIcons = data.StatIcons;
        }
        foreach (StatIcon statIcon in statIcons)
        {
            if (stat == statIcon.stat)
                return statIcon.icon;
        }

        Debug.Log("No icon found for stat : " + stat);
        return null;
    }
}
