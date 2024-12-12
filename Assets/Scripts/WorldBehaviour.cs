using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// мотыляет обстаклы и запускает vfx
public class WorldBehaviour : MonoBehaviour
{
    public CartBehaviour cart;
    
    private void Start()
    {
        if (cart != null)
        {
            cart.OnHitObstacle += HandleHitObstacle;
            cart.OnHitWater += HandleHitWater;
        }
    }

    private void HandleHitObstacle(Collision collision)
    {
        Debug.Log($"Collision with: {collision.gameObject.name}");
        Destroy(collision.gameObject);
    }
    
    private void HandleHitWater(Vector3 coordinates)
    {
        Debug.Log($"Water hit: {coordinates.ToString()}");
    }

    private void OnDestroy()
    {
        if (cart != null)
        {
            cart.OnHitObstacle -= HandleHitObstacle;
        }
    }
}