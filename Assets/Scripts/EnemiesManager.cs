using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawnGroup
{
    public EnemyData enemyData;
    public int count;
    public bool isBoss;
    public float repeatTimer;
    public float timeBetweenSpawn;
    public int repeatCount;

    public EnemiesSpawnGroup (EnemyData enemyData, int count, bool isBoss)
    {
        this.enemyData = enemyData;
        this.count = count;
        this.isBoss = isBoss;
    }

    public void SetRepeatSpawn(float timeBetweenSpawns, int repeatCount)
    {
        this.timeBetweenSpawn = timeBetweenSpawns;
        this.repeatCount = repeatCount;
        repeatTimer = timeBetweenSpawn;
    }
}


public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] GameObject enemy;  
    [SerializeField] Vector2 spawnArea;
    GameObject player;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBossHealth;
    [SerializeField] Slider bossHealthBar;

    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeadtedSpawnGroupList;

    int spawnPerFrame = 2;
 

    private void Start()
    {
        player = GameManager.Instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHpBar>(true).GetComponent<Slider>();
        stageProgress = FindObjectOfType<StageProgress>();
    }

    private void Update()
    {
        ProcessSpawn();
        PrecessRepeatedSpawnGroups();
        UpdateBossHealth();
    }

    private void PrecessRepeatedSpawnGroups()
    {
        if(repeadtedSpawnGroupList == null) { return; }
        for(int i = repeadtedSpawnGroupList.Count - 1; i >= 0; i--)
        {
            repeadtedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if (repeadtedSpawnGroupList[i].repeatTimer < 0)
            {
                repeadtedSpawnGroupList[i].repeatTimer = repeadtedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeadtedSpawnGroupList[i].enemyData, repeadtedSpawnGroupList[i].count, repeadtedSpawnGroupList[i].isBoss);
                repeadtedSpawnGroupList[i].repeatCount -= 1;
                if (repeadtedSpawnGroupList[i].repeatCount <= 0)
                {
                    repeadtedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    private void ProcessSpawn()
    {
        if (enemiesSpawnGroupList == null) { return; }

        for (int i = 0; i < spawnPerFrame; i++)
        {
            if (enemiesSpawnGroupList.Count > 0)
            {
                if (enemiesSpawnGroupList[0].count <= 0) { return; }
                SpawnEnemy(enemiesSpawnGroupList[0].enemyData, enemiesSpawnGroupList[0].isBoss);
                enemiesSpawnGroupList[0].count -= 1;

                if (enemiesSpawnGroupList[0].count <= 0)
                {
                    enemiesSpawnGroupList.RemoveAt(0);
                }
            }
        } 
    }

    private void UpdateBossHealth()
    {
        if (bossEnemiesList == null) { return; }
        if(bossEnemiesList.Count == 0) { return; }

        currentBossHealth = 0;

        for(int i = 0; i < bossEnemiesList.Count; i++)
        {
            if (bossEnemiesList[i] == null) { continue; }
            currentBossHealth += bossEnemiesList[i].stats.hp;
        }

        bossHealthBar.value = currentBossHealth;

        if(currentBossHealth <= 0)
        {
            bossHealthBar.gameObject.SetActive(false);
            bossEnemiesList.Clear();
        }
    }

    public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)
    {
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);

        if(enemiesSpawnGroupList == null) { enemiesSpawnGroupList = new List<EnemiesSpawnGroup>(); }

        enemiesSpawnGroupList.Add(newGroupToSpawn);
    }



    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea);

        position += player.transform.position;

        //spawning main enemy object
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);

        if(isBoss == true)
        {
            SpawnBossEnemy(newEnemyComponent);
        }

        newEnemy.transform.parent = transform;


        //spawning sprite object of the enemy
        newEnemyComponent.InitSprite(enemyToSpawn.animatedPrefab);
        

    }

    private void SpawnBossEnemy(Enemy newBoss)
    {
        if(bossEnemiesList ==  null)
        {
            bossEnemiesList = new List<Enemy>();
        }

        bossEnemiesList.Add(newBoss);

        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }

    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);

        if(repeadtedSpawnGroupList == null)
        {
            repeadtedSpawnGroupList = new List<EnemiesSpawnGroup>();
        }

        repeadtedSpawnGroupList.Add(repeatSpawnGroup);
    }
}
