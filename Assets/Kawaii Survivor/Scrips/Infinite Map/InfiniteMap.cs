using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject mapChunkPrefab;

    [Header("Settings")]
    [SerializeField] private float mapChunkSize;
    // Start is called before the first frame update
    void Start() => GenarateMap();


    private void GenarateMap()
    {
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
                GenarateMapChunk(x, y);

    }

    private void GenarateMapChunk(int x, int y)
    {
        Vector3 spawnPosition = new Vector3(x, y) * mapChunkSize;
        Instantiate(mapChunkPrefab, spawnPosition, Quaternion.identity, transform);
    }
}
