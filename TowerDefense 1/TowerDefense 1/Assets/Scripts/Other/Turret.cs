using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    public float fireRate = 1f;
    public float range = 15f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fires")]

    public string EnemisTag = "Enemy";
    public float turnSpeed = 10f;
    private Transform partTorotate;
    private GameObject bulletPrefab;
    private Transform firePoint;
    private bool isCanLaser = false;
    public LineRenderer lineRenderer;
    private List<Enemy> enemys;
    void Awake()
    {
        isCanLaser = false ;
        partTorotate = transform.Find("Parttorotate");
        if (name == "AnotherTurret(Clone)")
        {
            bulletPrefab = Resources.Load<GameObject>("Prefab/Missile");
        }
        else if (name == "StandardTurret(Clone)")
        {
            bulletPrefab = Resources.Load<GameObject>("Prefab/Bullet");
        }
        else if (name == "LaserTurret(Clone)")
        {
            isCanLaser = true;
            enemys = new List<Enemy>();
        }
        firePoint = partTorotate.Find("FirePoint");
        
    }
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemis = GameObject.FindGameObjectsWithTag(EnemisTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearsetEnemy = null;
        foreach (GameObject enemy in enemis)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearsetEnemy = enemy;
            } 
        }
        if (nearsetEnemy != null && shortesDistance <= range)
        {
            target = nearsetEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {
        if (target == null)
        {
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
                if (enemys.Count > 0)
                {
                    foreach (var enemy in enemys)
                    {
                        enemy.LowerSpeed(false);
                    }
                }
            }
            return;
        } 
        LockTarget();
        if (isCanLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        } 
    }
    void Laser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        target.GetComponent<Enemy>().BeingHit(0.35f);
        target.GetComponent<Enemy>().LowerSpeed(true);
        if (!enemys.Contains(target.GetComponent<Enemy>()))
        {
            enemys.Add(target.GetComponent<Enemy>());
        }
    }
    void LockTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partTorotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partTorotate.rotation = Quaternion.Euler(0, rotation.y, 0);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Shoot()
    {
        GameObject bullIns = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bullIns.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target.transform);
        }
    }
}
