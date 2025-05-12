using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Tilemaps;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Attacks")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;

    protected float attackTimer;

    [Header("Animations")]
    [SerializeField] protected float armLerp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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

        if (Random.Range(0, 101) <= 50)
        {
            isCriticalHit = true;
            return damage*2;
        }

        return damage;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}
