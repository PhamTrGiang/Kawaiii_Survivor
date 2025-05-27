using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Elements")]
    private Rigidbody2D rig;

    [Header("Settings")]
    [SerializeField] private float baseMoveSpeed;
    private float moveSpeed;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        rig.velocity = InputManager.instance.GetMoveVector() * moveSpeed * Time.deltaTime;
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float _moveSpeedPercent = playerStatsManager.GetStatValue(Stat.MoveSpeed) / 100;
        moveSpeed = baseMoveSpeed * (1 + _moveSpeedPercent);
    }
}
