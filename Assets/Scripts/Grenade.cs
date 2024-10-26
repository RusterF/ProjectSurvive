using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public ItemData itemData; // Reference to the item's data, populated when instantiated

    private bool hasExploded = false;

    void Explode()
    {
        if (hasExploded) return; // Prevent multiple explosions

        // Instantiate explosion effect at grenade's position
        if (itemData.explosionEffect != null)
        {
            Instantiate(itemData.explosionEffect, transform.position, transform.rotation);
        }

        // Apply explosion effect to nearby objects
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, itemData.radius);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            // Check if the object has a destructible component
            Destructible destructible = nearbyObject.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.Destroy();
            }
        }

        // Apply explosion force to nearby objects with rigidbody
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, itemData.radius);
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(itemData.force, transform.position, itemData.radius);
            }
        }

        hasExploded = true;
        Destroy(gameObject); // Destroy grenade after explosion
    }

    // Trigger explosion on collision
    void OnTriggerEnter(Collider other)
    {
        if (hasExploded) return;

        // Prevent grenade from exploding on the ground immediately
        if (other.CompareTag("Ground")) return;

        Explode();
    }
}
