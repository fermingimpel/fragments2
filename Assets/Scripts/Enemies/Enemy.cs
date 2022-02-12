using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    [SerializeField] float health;
    [SerializeField] float speed;

    [SerializeField] float damage;
    [SerializeField] float distanceToAttack;

    [SerializeField] ParticleSystem hitParticles;

    [SerializeField] NavMeshAgent pathfinding;

    [SerializeField] PlayerController player;

    [SerializeField] AmmoBox ammoBox;

    public enum EnemyState {
        Alive, Dead
    }
    [SerializeField] EnemyState enemyState;

    bool canInteract = true;

    private void Start() {
        player = FindObjectOfType<PlayerController>();
        pathfinding.speed = speed;
    }

    void Update() {
        if (enemyState == EnemyState.Dead)
            return;

        if (!canInteract)
            return;

        if (Vector3.Distance(player.transform.position, transform.position) <= distanceToAttack)
            Attack();

        if (enemyState == EnemyState.Alive) {
            pathfinding.destination = player.transform.position;
            transform.LookAt(new Vector3(pathfinding.destination.x, transform.position.y, pathfinding.destination.z));
        }
    }

    public void Hit(float damage, Vector3 hitPos, Vector3 attackerPos) {
        if (enemyState == EnemyState.Dead)
            return;

        hitParticles.transform.position = hitPos;
        hitParticles.transform.LookAt(attackerPos);
        hitParticles.Play();

        health -= damage;
        if(health <= 0) 
            Die();
    }

    void Attack() {
        StopMovement();

        player.Hit(damage);
        
        enemyState = EnemyState.Dead;
        Destroy(this.gameObject);

    }

    void Die() {
        StopMovement();

        enemyState = EnemyState.Dead;
        Destroy(this.gameObject);
        
        if(Random.Range(0,2) != 0)
            Instantiate(ammoBox, transform.position + Vector3.down, Quaternion.identity);
    }

    void StopMovement() {
        canInteract = false;
        pathfinding.isStopped = true;
    }

}
