using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [SerializeField] private float speed = 5f; // Kecepatan musuh
    private Vector2 screenBounds; // Batas layar
    private Vector2 direction; // Arah gerakan musuh
    private SpriteRenderer spriteRenderer; // Referensi SpriteRenderer
    private InvincibilityComponent invincibilityComponent; // Referensi komponen invincibility
    private AttackComponent attackComponent; // Referensi komponen serangan

    void Start()
    {
        // Inisialisasi komponen-komponen
        spriteRenderer = GetComponent<SpriteRenderer>();
        invincibilityComponent = GetComponent<InvincibilityComponent>();
        attackComponent = GetComponent<AttackComponent>();

        // Tentukan batas layar
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Respawn(); // Tentukan posisi awal musuh
    }

    void Update()
    {
        // Gerakkan musuh
        transform.Translate(direction * speed * Time.deltaTime);

        // Jika musuh keluar dari batas layar, lakukan respawn
        if (transform.position.x < -screenBounds.x - 1 || transform.position.x > screenBounds.x + 1)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Tentukan arah gerakan berdasarkan posisi respawn
        float spawnPositionX;
        if (Random.value > 0.5f)
        {
            spawnPositionX = screenBounds.x;
            direction = Vector2.left;
        }
        else
        {
            spawnPositionX = -screenBounds.x;
            direction = Vector2.right;
        }

        // Tetapkan posisi baru musuh secara acak pada sumbu Y
        transform.position = new Vector2(spawnPositionX, Random.Range(-screenBounds.y + 1f, screenBounds.y - 1f));
    }
}
