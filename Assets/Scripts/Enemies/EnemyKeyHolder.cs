using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeyHolder : Enemy {
    
    [SerializeField] Transform key;

    private GameObject spawnedKey;
    protected override void Die() {
        SpawnKey();
        DestroyEnemy(deathSound);
    }

    protected override void Attack() {
        SpawnKey();
        base.Attack();
    }

    public void SetKey(Transform k) {
        key = k;
    }

    void SpawnKey() {
        if (key != null) {
            key.gameObject.SetActive(true);
            key.position = transform.position + Vector3.down * 1.3f;
        }
    }
}
