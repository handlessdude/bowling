using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// viewModel для канваса
public class UIBehaviour : MonoBehaviour
{
    public InputBehaviour input;
    
    public WaterBehaviour water;

    public int score = 0;
    
    private void Start()
    {
        if (input != null)
        {
            input.OnUpdateLaunchPower += HandleUpdateLaunchPower;
        }  
        if (water != null)
        {
            water.OnEnterWater += HandleWaterEnter;
        }
    }

    private void HandleWaterEnter(GameObject gameObject)
    {
        if (gameObject.CompareTag("Obstacle"))
        {
            score += 1;
            Debug.Log($"Score: {score}");
        }
        else if (gameObject.CompareTag("Cart"))
        {
            Debug.Log($"The cart has entered the water.");
        }
    }
    
    private void HandleUpdateLaunchPower(float power)
    {
        // Debug.Log($"Update launch power: {power}");
    }

    private void OnDestroy()
    {
        if (input != null)
        {
            input.OnUpdateLaunchPower -= HandleUpdateLaunchPower;
        }  
        if (water != null)
        {
            water.OnEnterWater -= HandleWaterEnter;
        }
    }
}