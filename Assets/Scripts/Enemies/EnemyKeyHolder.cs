using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeyHolder : Enemy {
    
    [SerializeField] GameObject key;

    private GameObject spawnedKey;
    protected override void Die() {
        if (key != null)
        {
            spawnedKey = Instantiate(key, Vector3.zero, Quaternion.identity);
            spawnedKey.transform.position = transform.position + Vector3.down * 1.3f;
        }
        DestroyEnemy(deathSound);
    }

    protected override void Attack() {
        if (key != null)
        {
            spawnedKey = Instantiate(key, Vector3.zero, Quaternion.identity);
            spawnedKey.transform.position = transform.position + Vector3.down * 1.3f;
        }
        base.Attack();
    }

    public void SetKey(GameObject k) {
        key = k;
    }

}
