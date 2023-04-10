using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField][Range(0.1f, 5f)] float startSpeed = 0.2f;

    [ReadOnly] public float speed;

    [SerializeField][Range(20f, 1000f)] float startHealth = 100;
    [ReadOnly] public float health;

    [SerializeField][Range(5f, 600f)] int worth = 5;

    //public GameObject deathEffect;

    // TODO: Have a health bar 
    // [Header("Unity Stuff")]
    //[SerializeField] Image healthBar;

    bool isDead = false;

    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }

    private void OnEnable()
    {
        // TODO: Set agent's speed instead of depricated speed
        speed = startSpeed;
        health = startHealth;
        isDead = false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        // TODO: Decrease players health
        //healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            RewardCoin();
            Die();
        }
    }

    public void Slow(float pct)
    {
        // TODO: Slow agent's speed and not depricated speed
        speed = startSpeed * (1f - pct);
    }

    public void Die()
    {
        isDead = true;

        // TODO: Play death effect on enemy's death
        //GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        gameObject.SetActive(false);
    }

    void RewardCoin()
    {
        if (bank == null) return;
        bank.Deposit(worth);
    }
}
