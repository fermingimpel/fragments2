using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeyHolder : Enemy {
    
    [SerializeField] Transform key;
    protected override void Start() {
        base.Start();
        key = FindObjectOfType<Key>().transform;
    }

    protected override void Die() {
        if(key!=null)
            key.position = transform.position + Vector3.down * 1.3f;
        DestroyEnemy(deathSound);
    }

    protected override void Attack() {
        if(key!=null)
            key.position = transform.position + Vector3.down * 1.3f;
        base.Attack();
    }

}
