using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform _target;

    float _speed;
    int _damage;

    // TODO: Add hit effect
    //[SerializeField] GameObject impactEffect;

    public void Seek(Transform target, int damage, float speed)
    {
        _target = target;
        _damage = damage;
        _speed = speed;
    }

    void Update()
    {

        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_target);

    }

    public virtual void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);
        Damage(_target);
        Destroy(gameObject);
    }

    public void Damage(Transform damageable)
    {
        IDamageable<float> e = damageable.GetComponent<IDamageable<float>>();
        if (e != null)
        {
            e.Damage(_damage);
        }
    }
}
