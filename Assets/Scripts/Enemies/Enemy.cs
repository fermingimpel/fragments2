using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour {

    [SerializeField] protected float health;
    [SerializeField] protected float speed;

    [SerializeField] protected float damage;
    [SerializeField] protected float distanceToAttack;

    [SerializeField] protected ParticleSystem hitParticles;

    [SerializeField] protected NavMeshAgent pathfinding;
    [SerializeField] protected PlayerController player;

    [SerializeField] protected AmmoBox ammoBox;

    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected  AudioClip deathSound;
    [SerializeField] protected AudioClip attackSound;

    public enum EnemyState {
        Alive, Dead
    }
    [SerializeField] EnemyState enemyState;

    bool canInteract = true;

    public static Action<Enemy> EnemyDead;

    void Awake() {
        PauseController.Pause += Pause;
    }
   protected virtual void Start() {
        player = FindObjectOfType<PlayerController>();
        pathfinding.speed = speed;
    }

    void OnDestroy() {
        PauseController.Pause -= Pause;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

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

   protected virtual void Attack() {
        player.Hit(damage);
        DestroyEnemy(attackSound);
    }

    protected virtual void Die() {
        if(UnityEngine.Random.Range(0,2) != 0)
            Instantiate(ammoBox, transform.position + Vector3.down, Quaternion.identity);
        DestroyEnemy(deathSound);
    }

    void StopMovement() {
        canInteract = false;
        pathfinding.isStopped = true;
    }

    protected void DestroyEnemy(AudioClip audio) {
        StopMovement();

        GetComponent<BoxCollider>().enabled = false;
        transform.localScale = Vector3.zero;
        enemyState = EnemyState.Dead;
        audioSource.PlayOneShot(audio);
        EnemyDead?.Invoke(this);
        Destroy(this.gameObject, audio.length);
    }

    void Pause() {
        if (PauseController.instance.IsPaused) {
            pathfinding.isStopped = true;
            canInteract = false;
            return;
        }

        canInteract = true;
        pathfinding.isStopped = false;
    }

}
