using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;
    public int totalPoints = 0; 

    void Start()
    {
        waveNumber = 0;
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.combatManager = this;
        }
    }

    void Update()
    {
        if (totalEnemies <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                timer = 0;
                StartNextWave();
            }
        }
    }

    private void StartNextWave()
    {
        timer = 0;
        waveNumber++;
        // Debug.Log("Starting wave " + waveNumber);
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            Debug.Log("Starting enemy spawner");
            enemySpawner.startSpawning();
        }
    }

    public void onDeath(Enemy enemy)
    {
        totalEnemies--;
        totalPoints += enemy.Level; // Update totalPoints based on enemy level
        Debug.Log("Total Points: " + totalPoints);
    }
}
