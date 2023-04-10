using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour
{
    Vector3 startPos;
    List<Transform> path = new List<Transform>();

    Transform targetWaypoint;
    int waypointIndex = -1;

    Enemy enemy;

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }

    void OnEnable()
    {
        path = Waypoints.Path;
        waypointIndex = -1;

        if (path.Count > 0)
        {
            targetWaypoint = path[0];
            transform.LookAt(targetWaypoint.position);
        }

        ReturnToStart();
        GetNextWaypoint();
        MoveToWaypoint();
    }

    void Update()
    {
        if (gameObject.activeInHierarchy && path.Count > 0)
            CheckWaypoint();
    }

    void ReturnToStart()
    {
        transform.position = startPos;
    }

    void CheckWaypoint()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    GetNextWaypoint();
                    if (gameObject.activeInHierarchy && path.Count > 0)
                        MoveToWaypoint();
                }
            }
        }
    }

    void MoveToWaypoint()
    {
        navMeshAgent.destination = targetWaypoint.position;
    }

    void GetNextWaypoint()
    {
        if (path.Count < 0) return;
        if (waypointIndex >= path.Count - 1)
        {
            EndPath();
            return;
        }
        targetWaypoint = path[++waypointIndex];
    }

    void EndPath()
    {
        //PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        enemy.Die();
    }
}
