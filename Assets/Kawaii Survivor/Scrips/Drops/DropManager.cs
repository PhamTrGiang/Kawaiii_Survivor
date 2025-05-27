using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
    [SerializeField] private Chest chestPrefab;

    [Header("Setting")]
    [SerializeField][Range(0, 100)] private int cashDropChance;
    [SerializeField][Range(0, 100)] private int chestDropChance;

    [Header("Pooling")]
    private ObjectPool<Candy> candyPool;
    private ObjectPool<Cash> cashPool;


    private void Awake()
    {
        Enemy.onPassAway += EnenyPassedAwayCallback;
        Enemy.onBossPassAway += BossEnemyPassedAwayCallback;

        Candy.onCollected += ReleaseCandy;
        Cash.onCollected += ReleaseCash;
    }

    private void OnDestroy()
    {
        Enemy.onPassAway -= EnenyPassedAwayCallback;
        Enemy.onBossPassAway -= BossEnemyPassedAwayCallback;

        Candy.onCollected -= ReleaseCandy;
        Cash.onCollected -= ReleaseCash;
    }



    // Start is called before the first frame update
    void Start()
    {
        candyPool = new ObjectPool<Candy>(
            CandyCreateFunction,
            CandyActionOnGet,
            CandyActionOnRelease,
            CandyActionOnDestroy);

        cashPool = new ObjectPool<Cash>(
            CashCreateFunction,
            CashActionOnGet,
            CashActionOnRelease,
            CashActionOnDestroy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Candy CandyCreateFunction() => Instantiate(candyPrefab, transform);
    private void CandyActionOnGet(Candy candy) => candy.gameObject.SetActive(true);
    private void CandyActionOnRelease(Candy candy) => candy.gameObject.SetActive(false);
    private void CandyActionOnDestroy(Candy candy) => Destroy(candy.gameObject);

    private Cash CashCreateFunction() => Instantiate(cashPrefab, transform);
    private void CashActionOnGet(Cash cash) => cash.gameObject.SetActive(true);
    private void CashActionOnRelease(Cash cash) => cash.gameObject.SetActive(false);
    private void CashActionOnDestroy(Cash cash) => Destroy(cash.gameObject);



    private void EnenyPassedAwayCallback(Vector2 enemyPosition)
    {
        bool shouldSpawnCash = Random.Range(0, 101) <= cashDropChance;

        DroppableCurrency droppable = shouldSpawnCash ? cashPool.Get() : candyPool.Get();
        droppable.transform.position = enemyPosition;

        TryDropChest(enemyPosition);
    }
    private void BossEnemyPassedAwayCallback(Vector2 bossPosition)
    {
        DropChest(bossPosition);

    }
    private void TryDropChest(Vector2 spawnPosition)
    {
        bool shouldSpawnChest = Random.Range(0, 101) <= chestDropChance;

        if (!shouldSpawnChest)
            return;

        DropChest(spawnPosition);
    }

    private void DropChest(Vector2 spawnPosition)
    {
        Instantiate(chestPrefab, spawnPosition, Quaternion.identity, transform);
    }

    private void ReleaseCandy(Candy candy) => candyPool.Release(candy);
    private void ReleaseCash(Cash cash) => cashPool.Release(cash);
}
