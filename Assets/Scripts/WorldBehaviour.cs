using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 

public class WorldBehaviour : MonoBehaviour
{
    public CartBehaviour cart;

    public GameObject obstaclePrefab;

    public GameObject trackPlatform;
    
    private const int MIN_OBSTACLES_COUNT = 20;
    private const int MAX_OBSTACLES_COUNT = 40;
    private const float HEIGHT_ABOVE_TRACK = 2;
        
    private bool isPaused = false;
    
    private void Start()
    {
        if (cart != null)
        {
            cart.OnHitObstacle += HandleHitObstacle;
        }
    
        if (obstaclePrefab != null && trackPlatform != null)
        {
            SpawnTrackElements();
        }
    }

    private void HandleHitObstacle(Collision collision)
    {
        // Debug.Log($"Collision with: {collision.gameObject.name}");
    }

    private void SpawnTrackElements()
    {
        int obstacleCount = UnityEngine.Random.Range(MIN_OBSTACLES_COUNT, MAX_OBSTACLES_COUNT);
        
        Transform[] trackPieces = trackPlatform.GetComponentsInChildren<Transform>();
        
        List<Transform> validPlatforms = new List<Transform>();
        foreach (Transform piece in trackPieces)
        {
            if (piece != trackPlatform.transform) // exclude the parent itself
            {
                validPlatforms.Add(piece);
            }
        }
        
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 spawnPosition = GetRandomPointOnTrack(validPlatforms);
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity).tag = "Obstacle";
        }
    }
    
    private Vector3 GetRandomPointOnTrack(List<Transform> platforms)
    {
        Transform selectedPlatform = platforms[UnityEngine.Random.Range(0, platforms.Count)];
        
        Renderer renderer = selectedPlatform.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogWarning($"Track piece {selectedPlatform.name} has no Renderer. Skipping.");
            return Vector3.zero; // Fallback in case of no Renderer
        }
        Bounds platformBounds = renderer.bounds;
        
        float x = UnityEngine.Random.Range(platformBounds.min.x, platformBounds.max.x);
        float z = UnityEngine.Random.Range(platformBounds.min.z, platformBounds.max.z);
        // float y = platformBounds.max.y;
        float y = platformBounds.max.y + HEIGHT_ABOVE_TRACK;

        return new Vector3(x, y, z);
    }
    
    private void OnDestroy()
    {
        if (cart != null)
        {
            cart.OnHitObstacle -= HandleHitObstacle;
        }
    }
    
    public void ResetGame()
    {
        // закрыть меню
        // загрузить первую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ToggleIsPaused()
    {
         if (isPaused)
         {
             Time.timeScale = 1f;
             isPaused = false;
             Debug.Log("Game Resumed");
             // закрыть меню
             return;
         }
         Time.timeScale = 0f; 
         isPaused = true;
         Debug.Log("Game Paused");
         // открыть меню
    }
}