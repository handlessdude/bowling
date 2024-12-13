using UnityEngine;

public class VFXBehaviour : MonoBehaviour
{
    public ParticleSystem burstEffectPrefab;
    private float PARTICLE_SYSTEM_DEFAULT_DURATION = 2f;
    
    public CartBehaviour cart;
    
    private void Start()
    {
        if (cart != null)
        {
            cart.OnHitObstacle += HandleHitObstacle;
        }
    }

    private void HandleHitObstacle(Collision collision)
    {
        if (burstEffectPrefab != null)
        {
            // Get the contact point from the collision
            Vector3 collisionPoint = collision.contacts[0].point;

            if (burstEffectPrefab != null)
            { 
                var burstEffect = Instantiate(burstEffectPrefab, collisionPoint, Quaternion.identity);
                ParticleSystem ps = burstEffect.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    Destroy(burstEffect, ps.main.duration + ps.main.startLifetime.constantMax);
                }
                else
                {
                    Destroy(burstEffect, PARTICLE_SYSTEM_DEFAULT_DURATION);
                }       
            }
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