using UnityEngine;

public class WeaponShooter : MonoBehaviour
{

    [Header("Bullet Settings")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 1f;
    float fireCountdown = 0f;

    [SerializeField] GameObject firePoint;

    Transform target;

    public void TryShoot(Transform _target)
    {
        target = _target;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation, transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        firePoint.GetComponentInChildren<ParticleSystem>().Play();

        if (bullet != null)
            bullet.Seek(target);
    }
}
