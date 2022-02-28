﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour {

    [SerializeField] List<Enemy> enemiesCreated;
    [SerializeField] List<Transform> spawners;
    [SerializeField] Enemy enemyPrefab;

    [SerializeField] int initialEnemyCount;
    [SerializeField] int maxEnemyCount;
    [SerializeField] int enemyAdditionAmount;

    [SerializeField] int currentRound;
   // [SerializeField] int roundToSpawnObjective;
    [SerializeField] float timeBetweenRounds;

    [SerializeField] Enemy enemyKeyHolder;
    [SerializeField] int roundToSpawnEnemyKeyHolder;
    [SerializeField] bool enemyKeyHolderSpawned;

    [SerializeField] bool canSpawnEnemies = true;
    float spawnTimer = 0f;
    int enemyCount = 0;

    void Awake() {
        Enemy.EnemyDead += EnemyDead;
    }
    void Start() {
        enemyCount = initialEnemyCount;
    }

    void OnDisable() {
        Enemy.EnemyDead -= EnemyDead;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!canSpawnEnemies)
            return;

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= timeBetweenRounds) {
            SpawnHorde();
            spawnTimer = 0;
            canSpawnEnemies = false;
        }
    }

    void SpawnHorde() {
        if (!enemyKeyHolderSpawned) 
            if (currentRound == roundToSpawnEnemyKeyHolder) {
                int index = Random.Range(0, spawners.Count);
                Enemy e = Instantiate(enemyKeyHolder, spawners[index].position, Quaternion.identity);
                enemiesCreated.Add(e);
                enemyKeyHolderSpawned = true;
            }

        for(int i = 0; i < enemyCount; i++) {
            if(enemiesCreated.Count < enemyCount) {
                int index = Random.Range(0, spawners.Count);
                Enemy e = Instantiate(enemyPrefab, spawners[index].position, Quaternion.identity);
                enemiesCreated.Add(e);
            }
        }

        enemyCount += enemyAdditionAmount;
        if (enemyCount > maxEnemyCount)
            enemyCount = maxEnemyCount;
    }

    void EnemyDead(Enemy enemy) {
        enemiesCreated.Remove(enemy);
        if(enemiesCreated.Count <= 0) {
            currentRound++;
            canSpawnEnemies = true;
        }
    }
}
