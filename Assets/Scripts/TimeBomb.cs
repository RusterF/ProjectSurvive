using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    private BaseBomb mBomb;

    void Start()
    {
        mBomb = GetComponent<BaseBomb>();

        mBomb.onExplode += onExplode;
    }

    private void OnDestroy()
    {
        mBomb.onExplode -= onExplode;
    }

    private void onExplode(Collider[] colliders)
    {
        foreach (var collider in colliders)
        {
            ZombieController controller;
            collider.gameObject.TryGetComponent<ZombieController>(out controller);

            if (controller != null)
            {
                controller.SlowDown();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
