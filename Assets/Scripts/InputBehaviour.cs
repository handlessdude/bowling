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

    private float LAUNCH_POWER_MODIFIER = 1000f;
    
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            launchPower = 0;
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            launchPower += Time.deltaTime * LAUNCH_POWER_MODIFIER;
            OnUpdateLaunchPower?.Invoke(launchPower); // accelerationSlider.value = launchPower;
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            cart.Launch(launchPower);
            
            launchPower = 0;
            OnUpdateLaunchPower?.Invoke(launchPower); // accelerationSlider.value = 0;
        }
    }
}