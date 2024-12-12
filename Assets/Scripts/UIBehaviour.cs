using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// viewModel для канваса
public class UIBehaviour : MonoBehaviour
{
    public CartBehaviour cart;

    public int hitCount = 0;
    
    private void Start()
    {
        if (cart != null)
        {
            cart.OnHitObstacle += HandleHitObstacle;
        }
    }

    private void HandleHitObstacle(Collision collision)
    {
        hitCount += 1;
        
        Debug.Log($"Hit count: {hitCount}");
    }

    private void OnDestroy()
    {
        if (cart != null)
        {
            cart.OnHitObstacle -= HandleHitObstacle;
        }
    }
}