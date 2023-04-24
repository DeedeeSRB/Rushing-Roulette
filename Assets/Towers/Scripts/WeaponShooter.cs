using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    TowerScriptableObject _towerSO;

    GameObject _bulletPrefab;

    int _damange;
    float _fireRate;
    float _projectileSpeed;

    float _fireCountdown = 0f;
    GameObject _firePoint;
    Transform _target;

    void Start()
    {
        _towerSO = GetComponent<Tower>().towerSO;

        _bulletPrefab = _towerSO.projectilePrefab;

        _damange = _towerSO.damage;
        _fireRate = _towerSO.fireRate;
        _projectileSpeed = _towerSO.projectileSpeed;

        _firePoint = transform.GetChild(0).Find("Turret").Find("FirePoint").gameObject;
    }

    public void TryShoot(Transform target)
    {
        _target = target;
        if (_fireCountdown <= 0f)
        {
            Shoot();
            _fireCountdown = 1f / _fireRate;
        }

        _fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        // TODO: Create bullet object pool
        GameObject bulletGO = Instantiate(_bulletPrefab, _firePoint.transform.position, _firePoint.transform.rotation, transform);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        _firePoint.GetComponentInChildren<ParticleSystem>().Play();
        bullet.Seek(_target, _damange, _projectileSpeed);
    }
}
