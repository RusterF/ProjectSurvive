using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float radius = 5f;                   // Explosion radius
    public float force = 5000f;                 // Explosion force
    public GameObject explosionEffect;          // Explosion effect prefab

    private bool hasExploded = false;

    // Initialize the grenade properties from ItemData
    public void InitializeGrenade(float radius, float force, GameObject explosionEffectPrefab)
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

        // Apply explosion effect to nearby objects
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible destructible = nearbyObject.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.Destroy();
            }
        }

        // Apply explosion force to nearby objects with rigidbody
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

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
