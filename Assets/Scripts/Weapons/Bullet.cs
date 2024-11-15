using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetObjectPool(IObjectPool<Bullet> pool)
    {
        objectPool = pool;
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        objectPool.Release(this);
    }

    private void OnBecameInvisible()
    {
        // Pastikan bullet kembali ke pool ketika tidak terlihat
        if (gameObject.activeSelf && objectPool != null)
        {
            objectPool.Release(this);
        }
    }
}
    