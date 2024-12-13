using System;
using UnityEngine;

public class WinAreaBehaviour : MonoBehaviour
{
    public event Action<GameObject> OnEnter;
    
    public event Action<GameObject> OnExit;
    
    private void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit?.Invoke(other.gameObject);
    }
}