using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform target;


    [Header("Settings")]
    [SerializeField] private Vector2 minMaxXY;
    private void LateUpdate()
    {

        if (target == null)
        {
            Debug.LogWarning("No target has been specified...");
            return;
        }

        Vector3 targetPosition = target.position;
        targetPosition.z = -10;


        if (!GameManager.instance.UseInfiniteMap)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, -minMaxXY.x, minMaxXY.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -minMaxXY.y, minMaxXY.y);
        }

        transform.position = targetPosition;
    }
}
