using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponStatsCalculator
{
    public static Dictionary<Stat, float> GetStats(WeaponDataSO weaponData, int level)
    {
        float multiplier = 1 + (float)level / 3;

        Dictionary<Stat, float> calculatorStats = new Dictionary<Stat, float>();

        foreach (KeyValuePair<Stat, float> kvp in weaponData.BaseStats)
        {
            if (weaponData.Prefab.GetType() != typeof(RangeWeapon) && kvp.Key == Stat.Range)
                calculatorStats.Add(kvp.Key, kvp.Value);
            else
                calculatorStats.Add(kvp.Key, kvp.Value * multiplier);
        }

        return calculatorStats;
    }

    public static int GetPurchasePrice(WeaponDataSO weaponData, int level)
    {
        float multiplier = 1 + (float)level / 3;
        return (int)(weaponData.PurchasePrice * multiplier);
    }

    public static int GetRecyclePrice(WeaponDataSO weaponData, int level)
    {
        float multiplier = 1 + (float)level / 3;
        return (int)(weaponData.RecyclePrice * multiplier);
    }
}
