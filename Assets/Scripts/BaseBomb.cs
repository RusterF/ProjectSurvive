using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : MonoBehaviour
{
    public float radius = 5f;                   // Explosion radius
    public float force = 5000f;                 // Explosion force
    public GameObject explosionEffect;          // Explosion effect prefab
    public Action<Collider[]> onExplode;
  
    private bool hasExploded = false;

    // Initialize the grenade properties from ItemData
    public void InitializeBomb(float radius, float force, GameObject explosionEffectPrefab)
    {
        this.radius = radius;
        this.force = force;
        this.explosionEffect = explosionEffectPrefab;
    }

    void Explode()
    {
        if (hasExploded) return; // Prevent multiple explosions

        // Instantiate explosion effect at grenade's position
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        if (force != 0)
        {
            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
        }

        onExplode(colliders);

        hasExploded = true;
        Destroy(gameObject); // Destroy grenade after explosion
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasExploded) return;

        if (other.CompareTag("Ground")) return;

        Explode();
    }
}
