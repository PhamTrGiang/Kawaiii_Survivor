using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteChildMover : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform playerTransform;

    [Header("Settings")]
    [SerializeField] private float mapChunkSize;
    [SerializeField] private float distanceThreshold = 1.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateChildren();
    }

    private void UpdateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            float calculatorDistanceThreshold = mapChunkSize * distanceThreshold;

            Vector3 distance = playerTransform.position - child.position;
            if (Mathf.Abs(distance.x) > calculatorDistanceThreshold)
                child.position += Vector3.right * calculatorDistanceThreshold * 2 * Mathf.Sign(distance.x);
            if (Mathf.Abs(distance.y) > calculatorDistanceThreshold)
                child.position += Vector3.up * calculatorDistanceThreshold * 2 * Mathf.Sign(distance.y);
        }
    }
}
