using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Level;
    public EnemySpawner enemySpawner;
    public CombatManager combatManager;

    private void Start()
    {
        if (combatManager != null)
        {
            Level = combatManager.waveNumber;
        }
    }

    private void OnDestroy()
    {
        if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.onDeath();
            combatManager.onDeath(this); 
        }
    }
}