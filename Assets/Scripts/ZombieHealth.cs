using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int currentHealth;
    public int zombieMoney;

    void Start()
    {
        
    }

    public void DecreaseHealth(int by)
    {
        if (currentHealth <= 0) return;

        currentHealth = Math.Max(currentHealth - by, 0);

        if (currentHealth <= 0)
        {
            FindFirstObjectByType<Player>().money += zombieMoney;
            StartDeath();
        }
    }

    public void StartDeath()
    {
        // Start animasi mati

        StartCoroutine(DestroyDelay());
        
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
