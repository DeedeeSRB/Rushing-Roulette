using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float range = 5f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] bool alive = true;
    [SerializeField] string enemyTag = "Enemy";

    Transform target;
    Transform weapon;
    WeaponShooter weaponShooter;

    GameObject[] enemies;
    float shortestDistance;
    GameObject nearestEnemy;
    float disToEnemy;

    void Start()
    {
        StartCoroutine(FindTarget());
        weapon = transform.GetChild(0);
        weaponShooter = gameObject.GetComponent<WeaponShooter>();
    }

    void Update()
    {
        if (target == null) return;

        AimWeapon();
        weaponShooter.TryShoot(target);
    }

    IEnumerator FindTarget()
    {
        while (alive)
        {
            enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            shortestDistance = Mathf.Infinity;
            nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                disToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (disToEnemy < shortestDistance)
                {
                    shortestDistance = disToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
                target = nearestEnemy.transform;
            else
                target = null;


            yield return new WaitForSeconds(0.5f);
        }
    }

    void AimWeapon()
    {
        if (target == null) return;

        Vector3 dir = target.position - weapon.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(weapon.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        weapon.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // weapon.LookAt(target); // This is very snapy
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
