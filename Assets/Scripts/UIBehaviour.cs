using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// viewModel для канваса
public class UIBehaviour : MonoBehaviour
{
    public Canvas gameStateCanvas;
    
    public Canvas menuCanvas;

    public WorldBehaviour world;
        
    public InputBehaviour input;
    
    public WaterBehaviour water;

    public WinAreaBehaviour winArea;
    
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
        if (world != null)
        {
            world.OnUpdateIsGamePaused += HandleUpdateIsGamePaused;
        }
        if (winArea != null)
        {
            winArea.OnEnter += HandleWinAreaEnter;
        }
    }

    private void HandleUpdateIsGamePaused(bool isPaused)
    {
        if (isPaused)
        {
            menuCanvas.gameObject.SetActive(true);
            gameStateCanvas.gameObject.SetActive(false);
            return;
        }
        menuCanvas.gameObject.SetActive(false);
        gameStateCanvas.gameObject.SetActive(true);
    }
    
    private void HandleWaterEnter(GameObject gameObject)
    {
        if (gameObject.CompareTag("Cart"))
        {
            Debug.Log($"The cart has entered the water.");
        }
    }
    
    private void HandleWinAreaEnter(GameObject gameObject)
    {
        if (gameObject.CompareTag("Obstacle"))
        {
            score += 1;
           
            if (scoreText != null)
            {
                scoreText.text = $"Score: {score}";
            }
        }
        if (gameObject.CompareTag("Cart"))
        {
            Debug.Log($"The cart has entered THE WIN AREA!");
        }
    }
    
    private void HandleUpdateLaunchPower(float power)
    {
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
        if (world != null)
        {
            world.OnUpdateIsGamePaused -= HandleUpdateIsGamePaused;
        }
        if (winArea != null)
        {
            winArea.OnEnter -= HandleWinAreaEnter;
        }
    }
}