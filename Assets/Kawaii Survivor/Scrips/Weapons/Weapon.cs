using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes.Test;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPlayerStatsDependency
{
    [field: SerializeField] public WeaponDataSO WeaponData { get; private set; }

    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Attacks")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;

    protected float attackTimer;

    [Header("Critical")]
    protected int criticalChance;
    protected float criticalPercent;

    [Header("Animations")]
    [SerializeField] protected float armLerp;

    public int Level { get; private set; }

    protected Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if (enemies.Length <= 0)
            return null;


        float minDistance = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enmyCheck = enemies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, enmyCheck.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enmyCheck;
                minDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    protected int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        if (Random.Range(0, 101) <= criticalChance)
        {
            isCriticalHit = true;
            return Mathf.RoundToInt(damage * criticalPercent);
        }

        return damage;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    public abstract void UpdateStats(PlayerStatsManager playerStatsManager);

    protected void ConfigureStats()
    {
        float multiplier = 1 + (float)Level / 3;
        damage = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.Attack) * multiplier);
        attackDelay = 1f / (WeaponData.GetStatValue(Stat.AttackSpeed) * multiplier);

        criticalChance = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.CriticalChance) * multiplier);
        criticalPercent = WeaponData.GetStatValue(Stat.CriticalPercent) * multiplier;

        if (WeaponData.Prefab.GetType() == typeof(RangeWeapon))
            range = WeaponData.GetStatValue(Stat.Range) * multiplier;
    }

    public void UpgradeTo(int targetLevel)
    {
        Level = targetLevel;
        ConfigureStats();
    }
}
