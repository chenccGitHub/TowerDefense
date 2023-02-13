using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHENCONGCONG;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private float speed = 6f;
    private Transform target;
    private int waypointsIndex = 0;
    public float hp = 200;
    public int damage = 1;
    public Image hpImage;
    public float hpMax = 200;
    void Start()
    {
        hp = hpMax;
        target = Waypoints.points[0];
    }
    void Update()
    {
        Vector3 position = target.position - transform.position;
        transform.Translate(position.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) < speed * 0.01f)
        {
            GetNextWaypoint();
        }
    }
    private void GetNextWaypoint()
    {
        if (waypointsIndex >= Waypoints.points.Length - 1)
        {
            PlayerStats.CalculateHp(damage);
            return;
        }
        waypointsIndex++;
        target = Waypoints.points[waypointsIndex];
    }
    public void BeingHit(float damage)
    {
        if (hp - damage <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            hp -= damage;
            SetHpImageShow();
        }
    }
    private void SetHpImageShow()
    {
        hpImage.fillAmount = hp / hpMax;
    }
}
