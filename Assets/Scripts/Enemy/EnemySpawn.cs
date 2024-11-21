using UnityEngine;
using UnityEngine.Assertions;


public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyVariants;
    [SerializeField] private int selectedVariant = 0;
    [SerializeField] private int level;

    void Start()
    {
       Assert.IsTrue(enemyVariants.Length > 0, "Tambahkan setidaknya 1 Prefab Enemy terlebih dahulu!");


    }

    private void Update()
    {
        for (int i = 1; i <= enemyVariants.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedVariant = i - 1;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (selectedVariant < enemyVariants.Length)
        {
            Enemy enemy = Instantiate(enemyVariants[selectedVariant]);
        }
    }
}