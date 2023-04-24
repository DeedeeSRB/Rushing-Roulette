using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    Tower _tower;
    WeaponShooter _weaponShooter;

    float _range;
    float _turnSpeed;
    string _enemyTag = "Enemy";

    Transform _target;
    Transform _weapon;

    GameObject[] _enemies;
    float _shortestDistance;
    GameObject _nearestEnemy;
    float _disToEnemy;

    void Start()
    {
        _tower = gameObject.GetComponent<Tower>();
        _weapon = transform.GetChild(0).GetChild(0);
        _weaponShooter = gameObject.GetComponent<WeaponShooter>();

        _range = _tower.towerSO.range;
        _turnSpeed = _tower.towerSO.turnSpeed;

        StartCoroutine(FindTarget());
    }

    void Update()
    {
        if (_target == null) return;

        AimWeapon();
        _weaponShooter.TryShoot(_target);
    }

    IEnumerator FindTarget()
    {
        while (_tower.alive)
        {
            _enemies = GameObject.FindGameObjectsWithTag(_enemyTag);
            _shortestDistance = Mathf.Infinity;
            _nearestEnemy = null;

            foreach (GameObject enemy in _enemies)
            {
                _disToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (_disToEnemy < _shortestDistance)
                {
                    _shortestDistance = _disToEnemy;
                    _nearestEnemy = enemy;
                }
            }

            if (_nearestEnemy != null && _shortestDistance <= _range)
                _target = _nearestEnemy.transform;
            else
                _target = null;


            yield return new WaitForSeconds(0.5f);
        }
    }

    void AimWeapon()
    {
        if (_target == null) return;

        Vector3 dir = _target.position - _weapon.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_weapon.rotation, lookRotation, Time.deltaTime * _turnSpeed).eulerAngles;
        _weapon.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // _weapon.LookAt(_target); // This is very snapy
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
