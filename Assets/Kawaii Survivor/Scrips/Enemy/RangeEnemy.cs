using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]
public class RangeEnemy : MonoBehaviour
{
    [Header("Compenents")]
    private EnemyMovement movement;
    private RangeEnemyAttack attack;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int health;


    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Relasted")]
    [SerializeField] private SpriteRenderer enenmyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private Collider2D collider;
    private bool hasSpawned;


    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;


    [Header("Attack")]
    [SerializeField] private float playerDetectionRadius;

    [Header("Actions")]
    public static Action<int, Vector2> onDamageTaken;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<RangeEnemyAttack>();

        player = FindFirstObjectByType<Player>();

        attack.StorePlayer(player);

        if (player == null)
        {
            Debug.LogWarning("No player found, Auto-Destroying...");
            Destroy(gameObject);
        }

        StartSpawnSepuence();
    }

    private void StartSpawnSepuence()
    {
        SetRenderersVisibility(false);

        //Scale up & down the spawn indicator
        Vector2 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceComplete);
    }

    private void SpawnSequenceComplete()
    {
        SetRenderersVisibility(true);
        hasSpawned = true;

        collider.enabled = true;

        movement.StorePlayer(player);
    }

    private void SetRenderersVisibility(bool visibility)
    {
        enenmyRenderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }
    // Update is called once per frame
    void Update()
    {
        if(!enenmyRenderer.enabled)
            return;

        ManagerAttack();

    }

    private void ManagerAttack(){
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > playerDetectionRadius)
            movement.FollowPlayer();
        else
            TryAttack();
 
    }

    private void TryAttack()
    {
        attack.AutoArm();
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;


        onDamageTaken?.Invoke(damage, transform.position);

        if (health <= 0)
            PassAway();
    }
    private void PassAway()
    {
        // Unparent the particle & play them
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();

        Destroy(gameObject);

    }

    void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

    }
}
