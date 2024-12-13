using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    public CartBehaviour cart;
    
    private float launchPower = 0f;
    private bool isCharging = false;
    
    public event Action<float> OnUpdateLaunchPower;

    private float LAUNCH_POWER_MODIFIER = 100f;
    private float LAUNCH_ANGLE_MODIFIER = 1000f;
    
    private float MIN_LAUNCH_POWER = 0f;
    private float MAX_LAUNCH_POWER = 200f;
    
    private Quaternion initialRotation;
    
    private float launchAngle = 0f; // in degrees
    
    void Start()
    {
        if (cart != null)
        {
            initialRotation = cart.transform.rotation;
        }
    }
    
    void Update()
    {
        HandleInput();
    }

    private void UpdateLaunchPower()
    {
        var newValue = launchPower + Time.deltaTime * LAUNCH_POWER_MODIFIER;
        launchPower = Mathf.Clamp(newValue, MIN_LAUNCH_POWER, MAX_LAUNCH_POWER);
        OnUpdateLaunchPower?.Invoke(launchPower);
    }
    
    private void UpdateLaunchAngle()
    {
        float mouseDelta = Input.GetAxis("Mouse X") * Time.deltaTime * LAUNCH_ANGLE_MODIFIER;
        launchAngle = Mathf.Clamp(launchAngle + mouseDelta, -45f, 45f);
            
        if (cart != null)
        {
            Quaternion rotation = Quaternion.Euler(0, launchAngle, 0);
            cart.transform.rotation = rotation * initialRotation;
        }
    }
    
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            launchPower = 0;
            launchAngle = 0;
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            UpdateLaunchPower();
            
            UpdateLaunchAngle();
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            
            if (cart != null)
            {
                cart.Launch(launchPower);
            }
            
            launchPower = 0;
            OnUpdateLaunchPower?.Invoke(launchPower);
        }
    }
}