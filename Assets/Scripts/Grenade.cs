using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private BaseBomb mBomb;

    private void Start()
    {
        mBomb = GetComponent<BaseBomb>();
        mBomb.onExplode += onExplode;
    }


    private void onExplode(Collider[] colliders)
    {
        foreach (Collider nearbyObject in colliders)
        {
            Destructible destructible = nearbyObject.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.Destroy();
            }

            ZombieHealth zombieHealth;

            nearbyObject.TryGetComponent<ZombieHealth>(out zombieHealth);

            if (zombieHealth != null)
            {
                zombieHealth.DecreaseHealth(300);
            }
        }
    }
}
