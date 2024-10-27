using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private Dictionary<ZombieHealth, Coroutine> mCoroutines;

    void Start()
    {
        mCoroutines = new Dictionary<ZombieHealth, Coroutine>();
    }

    private void OnDestroy()
    {
        mCoroutines.Clear();
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PoissionTimer(ZombieHealth health)
    {
        while (true)
        {
            health.DecreaseHealth(10);

            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ZombieHealth health;
        other.TryGetComponent(out health);

        if (health == null) return;

        if (mCoroutines.ContainsKey(health)) return;

        mCoroutines.Add(health, StartCoroutine(PoissionTimer(health)));
    }

    private void OnTriggerExit(Collider other)
    {
        ZombieHealth health;
        other.TryGetComponent(out health);

        if (health == null) return;

        if (!mCoroutines.ContainsKey(health)) return;

        StopCoroutine(mCoroutines[health]);
        mCoroutines.Remove(health);
    }
}
