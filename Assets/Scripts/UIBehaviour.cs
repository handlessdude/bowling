using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// viewModel для канваса
public class UIBehaviour : MonoBehaviour
{
    public InputBehaviour input;
    
    public WaterBehaviour water;
    
    public int score = 0;
    
    public TextMeshProUGUI scoreText;
    
    public Slider launchPowerSlider;
    
    private void Start()
    {
        // scoreText = GameObject.FindGameObjectWithTag("Score")?.GetComponent<TextMeshProUGUI>();
        // launchPowerSlider = GameObject.FindGameObjectWithTag("LaunchPower")?.GetComponent<Slider>();

        if (scoreText == null)
        {
            Debug.LogError("No TextMeshProUGUI element with the tag 'Score' found.");
        }
        if (launchPowerSlider == null)
        {
            Debug.LogError("No Slider element with the tag 'LaunchPower' found.");
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
           
            if (scoreText != null)
            {
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
        Debug.Log(power);
        if (launchPowerSlider != null)
        {
            launchPowerSlider.value = power;
        }
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