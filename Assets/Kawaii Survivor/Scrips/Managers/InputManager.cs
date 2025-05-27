using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Elements")]
    [SerializeField] private MobileJoystick playerJoystick;

    [Header("Setting")]
    [SerializeField] private bool forceHandheld;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if (SystemInfo.deviceType == DeviceType.Desktop && !forceHandheld)
            playerJoystick.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetMoveVector()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop && !forceHandheld)
            return GetDesktopMoveVector();
        else
            return playerJoystick.GetMoveVector();
    }

    private Vector2 GetDesktopMoveVector()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
