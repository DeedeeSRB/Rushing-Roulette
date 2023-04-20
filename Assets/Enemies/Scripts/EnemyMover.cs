using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour, ISlowable, ISpeedable
{
    [SerializeField][Range(1f, 10f)] public float startSpeed = 2f;
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
        startPos = FindObjectOfType<WaveSpawner>().SpawnPoint.position;
    }

    void OnEnable()
    {
        ResetPath();
        ReturnToStart();
        GetNextWaypoint();
        ResetEnemy();
        MoveToWaypoint();
    }

    void Update()
    {
        if (gameObject.activeInHierarchy && path.Count > 0)
            CheckWaypoint();
    }

    void ResetEnemy()
    {
        if (path.Count > 0)
            transform.LookAt(targetWaypoint.position);
        navMeshAgent.speed = startSpeed;
    }

    void ResetPath()
    {
        path = Waypoints.Path;
        waypointIndex = -1;
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

    void GetNextWaypoint()
    {
        if (path.Count < 0 || waypointIndex >= path.Count - 1)
        {
            EndPath();
            return;
        }
        targetWaypoint = path[++waypointIndex];
    }

    void MoveToWaypoint()
    {
        navMeshAgent.destination = targetWaypoint.position;
    }

    void EndPath()
    {
        // TODO: Lose player health
        // PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        enemy.Kill();
    }

    public void SlowDown(float pct)
    {
        navMeshAgent.speed = startSpeed * (1f - pct);
    }

    public void SpeedUp(float pct)
    {
        navMeshAgent.speed = startSpeed * (1f + pct);
    }
}
