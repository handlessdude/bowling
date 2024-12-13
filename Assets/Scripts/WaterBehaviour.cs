using System;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    public event Action<GameObject> OnEnterWater;
    
    public event Action<GameObject> OnExitWater;
    
    private void OnTriggerEnter(Collider other)
    {
        OnEnterWater?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExitWater?.Invoke(other.gameObject);
    }
}