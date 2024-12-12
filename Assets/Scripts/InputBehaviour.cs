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
    
    private Quaternion initialRotation;
    
    private float launchAngle = 0f; // in degrees
    
    void Start()
    {
        if (cart != null)
        {
            initialRotation = cart.transform.rotation;
            Debug.Log($"initialForwardVector: {cart.transform.forward.ToString()}");
        }
    }
    
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
            launchAngle = 0;
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            // launch power
            launchPower += Time.deltaTime * LAUNCH_POWER_MODIFIER;
            OnUpdateLaunchPower?.Invoke(launchPower);
            
            // launch angle
            float mouseDelta = Input.GetAxis("Mouse X") * Time.deltaTime * LAUNCH_ANGLE_MODIFIER;
            launchAngle = Mathf.Clamp(launchAngle + mouseDelta, -45f, 45f);
            
            Debug.Log($"Update launchAngle: {launchAngle} | forward: {cart.transform.forward.ToString()}");
            
            if (cart != null)
            {
                Quaternion rotation = Quaternion.Euler(0, launchAngle, 0);
                cart.transform.rotation = rotation * initialRotation;
            }
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