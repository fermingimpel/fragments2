using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] EnemyKeyHolder enemyKeyHolder;
    [SerializeField] Transform key;
    [SerializeField] int roundToSpawnEnemyKeyHolder;
    [SerializeField] bool enemyKeyHolderSpawned;

    [SerializeField] bool canSpawnEnemies = true;

    [Space, SerializeField] AudioManager audioManager;

    bool isInHorde;

    float spawnTimer = 0f;
    int enemyCount = 0;

    private QuestManager questManager;

    private ObjectiveBase objective;

    void Awake() {
        Enemy.EnemyDead += EnemyDead;
    }
    void Start() {
        enemyCount = initialEnemyCount;
        objective = GetComponent<ObjectiveBase>();
        questManager = QuestManager.Instance;
        CheckPlayerSafeZone.PlayerInteract += PlayerInSafeZone;
    }

    void OnDisable() {
        Enemy.EnemyDead -= EnemyDead;
        CheckPlayerSafeZone.PlayerInteract -= PlayerInSafeZone;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (!canSpawnEnemies)
            return;

        if (isInHorde)
            return;

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= timeBetweenRounds) {
            SpawnHorde();
            spawnTimer = 0;
            canSpawnEnemies = false;
        }
    }

    void SpawnHorde() {
        audioManager.StartFightMusic();
        isInHorde = true;
        if (currentRound <= 0)
        {
            questManager.ActivateQuest(questManager.GetQuestByName("Survive"));
        }
        if (!enemyKeyHolderSpawned) 
            if (currentRound == roundToSpawnEnemyKeyHolder) {
                int index = Random.Range(0, spawners.Count);
                EnemyKeyHolder e = Instantiate(enemyKeyHolder, spawners[index].position, Quaternion.identity);
                enemiesCreated.Add(e);
                enemyKeyHolderSpawned = true;
                e.SetKey(key);
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

        if (!questManager) return;
        
        if(questManager.GetActiveQuest().Count<=0)
            questManager.ActivateQuest(questManager.GetQuestByName("Survive"));
    }

    

    void EnemyDead(Enemy enemy) {
        enemiesCreated.Remove(enemy);
        if(enemiesCreated.Count <= 0) {
            currentRound++;
            canSpawnEnemies = true;
            isInHorde = false;
            if(objective)
                objective.CompleteObjective();

            audioManager.StartAmbientMusic();
        }
    }

    void PlayerInSafeZone(bool isInSafeZone) {
        canSpawnEnemies = !isInSafeZone;
    }

    public bool GetIsInHorde() {
        return isInHorde;
    }

}
