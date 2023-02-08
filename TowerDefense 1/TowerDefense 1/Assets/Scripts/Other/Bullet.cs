using UnityEngine;
using CHENCONGCONG;
public class Bullet : MonoBehaviour
{
    public float speed = 20;
    private Transform target;
    public float offest = 0.8f;
    private GameObject impactEffect;
    public float explosionRadius = 0;
    public float damage = 0;
    void Awake()
    {
        if (name == "Missile(Clone)")
        {
            impactEffect = Resources.Load<GameObject>("Prefab/MissileEffect");
        }
        else if (name == "Bullet(Clone)")
        {
            impactEffect = Resources.Load<GameObject>("Prefab/BulletEffect");
        }
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 distance = target.position - transform.position;
        float fireDistance = speed * Time.deltaTime + offest;
        if (distance.magnitude < fireDistance)
        {
            HitTarget();
            return;
        }
        transform.Translate(distance * speed * Time.deltaTime, Space.World);
        transform.LookAt(target);

    }
    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target.gameObject, damage);
        }
        Destroy(gameObject);

    }
    void Damage(GameObject _target, float _damage)
    {
        _target.GetComponent<Enemy>().BeingHit(damage);
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.gameObject, damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
