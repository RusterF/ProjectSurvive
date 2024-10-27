using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private float mDefaultSpeed;
    private Coroutine mResetSlowDownCoroutine;
    private NavMeshAgent mNavMeshAgent;
    private ZombieHealth mZombieHealth;

    public Animator animator;

    void Start()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        mZombieHealth = GetComponent<ZombieHealth>();


        mNavMeshAgent.SetDestination(
            FindFirstObjectByType<Player>().gameObject.transform.position
        );
        mDefaultSpeed = mNavMeshAgent.speed;


        mZombieHealth.onDeath += onDeath;
    }

    private void OnDestroy()
    {
        mZombieHealth.onDeath -= onDeath;
    }

    void onDeath()
    {
        mNavMeshAgent.isStopped = true;
        // Memulai animasi dengan animator?
    }

    public void SlowDown()
    {
        if (mZombieHealth.currentHealth <= 0) return;

        if (mResetSlowDownCoroutine != null)
            StopCoroutine(mResetSlowDownCoroutine);

        mResetSlowDownCoroutine = StartCoroutine(DelayResetSpeed());
        mNavMeshAgent.speed = 0.1f;
    }

    public IEnumerator DelayResetSpeed()
    {
        yield return new WaitForSeconds(15);
        mNavMeshAgent.speed = mDefaultSpeed;
    }

    void Update()
    {
        
    }
}
