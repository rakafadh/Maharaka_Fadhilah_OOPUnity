using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;

public class EnemyHorizontal : Enemy
{
    public float speed = 5f;
    private Vector3 direction;
    private Vector3 spawnPoint;

    private SpriteRenderer spriteRenderer; // Add SpriteRenderer reference
    private InvincibilityComponent invincibilityComponent; // Add Invincibility reference
    private AttackComponent attackComponent; // Add AttackComponent reference

    public HealthComponent healthComponent;

    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();

        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (Random.value > 0.5f)
        {
            spawnPoint = new Vector3(-screenBounds.x + 1, Random.Range(-screenBounds.y + 1f, screenBounds.y), 0);
            direction = Vector3.left;
        }
        else
        {
            spawnPoint = new Vector3(screenBounds.x - 1, Random.Range(-screenBounds.y + 1f, screenBounds.y), 0);
            direction = Vector3.right;
        }
        transform.position = spawnPoint;
    }

    void Update()
    {
        // Move the enemy
        transform.Translate(direction * speed * Time.deltaTime);

     
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


        if (transform.position.x > screenBounds.x)
        {
            direction = -direction;
        }
        else if (transform.position.x < -screenBounds.x)
        {
            direction = -direction;
        }



    }
}