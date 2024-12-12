using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    
    public event Action<Collision> OnHitObstacle; // эмитит гейм обджект с которым столкнулись
    
    public event Action<Vector3> OnHitWater; // эмитит точку попадания в воду
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(float power)
    {
        rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            OnHitObstacle?.Invoke(collision);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            OnHitWater?.Invoke(this.transform.position);
        }
    }
}
