using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Compenents")]
    private EnemyMovement movement;

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Relasted")]
    [SerializeField] private SpriteRenderer enenmyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;


    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    private float attackDelay;
    private float attackTimer;

    private bool hasSpawned;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();

        player = FindFirstObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("No player found, Auto-Destroying...");
            Destroy(gameObject);
        }

        StartSpawnSepuence();
        attackDelay = 1 / attackFrequency;
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
        if (attackTimer >= attackDelay)
            TryAttack();
        else
            Wait();
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
            Attack();

    }
    private void Attack()
    {
        attackTimer = 0;

        player.TakeDamage(damage);
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
