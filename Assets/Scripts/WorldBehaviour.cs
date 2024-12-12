using System;
using UnityEngine;

public class WorldBehaviour : MonoBehaviour
{
    public CartBehaviour cart;

    public GameObject obstaclePrefab;

    private const int MIN_OBSTACLES_COUNT = 10;
    private const int MAX_OBSTACLES_COUNT = 20;
        
    private void Start()
    {
        if (cart != null)
        {
            cart.OnHitObstacle += HandleHitObstacle;
            cart.OnHitWater += HandleHitWater;
        }
    
        if (obstaclePrefab != null)
        {
            SpawnTrackElements();
        }
    }

    private void HandleHitObstacle(Collision collision)
    {
        Debug.Log($"Collision with: {collision.gameObject.name}");
    }
    
    private void HandleHitWater(Vector3 coordinates)
    {
        Debug.Log($"Water hit: {coordinates.ToString()}");
    }

    private void SpawnTrackElements()
    {
        int obstacleCount = UnityEngine.Random.Range(MIN_OBSTACLES_COUNT, MAX_OBSTACLES_COUNT);
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 4, UnityEngine.Random.Range(10f, 50f));
            Instantiate(obstaclePrefab, position, Quaternion.identity).tag = "Obstacle";
        }
    }
    
    private void OnDestroy()
    {
        if (cart != null)
        {
            cart.OnHitObstacle -= HandleHitObstacle;
        }
    }
}