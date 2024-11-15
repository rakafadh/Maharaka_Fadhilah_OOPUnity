using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    public Transform parentTransform;
    

    private void Awake()
    {
        // Pastikan `parentTransform` sudah terisi sebelum membuat pool
        if (parentTransform == null)
        {
          parentTransform = new GameObject("BulletParent").transform;
        }
        
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGet, OnRelease, OnDestroyBullet, collectionCheck, defaultCapacity, maxSize);
    }

    private void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootIntervalInSeconds);
        }
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, parentTransform);
        newBullet.gameObject.SetActive(false);
        newBullet.SetObjectPool(objectPool); // Pastikan bullet mengetahui pool-nya
        return newBullet;
    }

    private void OnGet(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            bullet.gameObject.SetActive(true);
        }
    }

    private void OnRelease(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.gameObject.SetActive(false);
        }
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

    public void Shoot()
    {
        Bullet bullet = objectPool.Get();
        if (bullet != null)
        {
            Debug.Log("Nembak");
            bullet.gameObject.SetActive(true); // Aktifkan peluru saat ditembak
        }
       
    }
}
