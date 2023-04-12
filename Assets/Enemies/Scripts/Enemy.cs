using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField][Range(1f, 10f)] float startSpeed = 0.2f;

    [SerializeField][Range(20f, 1000f)] float startHealth = 100;
    [ReadOnly] public float health;

    [SerializeField][Range(5f, 600f)] int worth = 5;

    // TODO: Add death effect
    // public GameObject deathEffect;

    // TODO: Have a health bar 
    // [Header("Unity Stuff")]
    // [SerializeField] Image healthBar;

    bool isDead = false;

    Bank bank;
    NavMeshAgent navMeshAgent;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        navMeshAgent.speed = startSpeed;
        health = startHealth;
        isDead = false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        // TODO: Decrease enemies health bar
        // healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            RewardCoin();
            Die();
        }
    }

    public void SlowDown(float pct)
    {
        navMeshAgent.speed = startSpeed * (1f - pct);
    }

    public void SpeedUp(float pct)
    {
        navMeshAgent.speed = startSpeed * (1f + pct);
    }

    public void Die()
    {
        isDead = true;

        // TODO: Play death effect on enemy's death
        // GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        gameObject.SetActive(false);
    }

    void RewardCoin()
    {
        if (bank == null) return;
        bank.Deposit(worth);
    }
}
