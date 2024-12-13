using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// viewModel для канваса
public class UIBehaviour : MonoBehaviour
{
    public InputBehaviour input;
    
    public WaterBehaviour water;

    public int score = 0;
    
    public TextMeshProUGUI scoreText;
    
    private void Start()
    {
        // scoreText = GameObject.FindGameObjectWithTag("Score")?.GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
        {
            Debug.LogError("No TextMeshProUGUI element with the tag 'Score' found.");
        }
        
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
           
            if (scoreText != null)
            {
                // Debug.Log($"Score: {score}");
                scoreText.text = $"Score: {score}";
            }
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