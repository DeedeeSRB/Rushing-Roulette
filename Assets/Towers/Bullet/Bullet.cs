using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;

    [SerializeField] float speed = 70f;

    public int damage = 50;

    //[SerializeField] GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    public virtual void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);
        Damage(target);
        Destroy(gameObject);
    }



   public void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        Debug.Log("LoL");
        if (e != null)
        {
            e.TakeDamage(damage);
            Debug.Log("BOOM");
            e.BleedingAnim(true);
        }
    }
}
