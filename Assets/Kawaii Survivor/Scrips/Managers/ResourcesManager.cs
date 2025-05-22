using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    const string statIconsDataPath = "Data/Stat Icons";
    const string objectDatasPath = "Data/Objects/";

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

    public static ObjectDataSO[] objectDatas;
    public static ObjectDataSO[] Objects
    {
        get
        {
            if (objectDatas == null)
                objectDatas = Resources.LoadAll<ObjectDataSO>(objectDatasPath);
            return objectDatas;

        }
        private set { }
    }
}
