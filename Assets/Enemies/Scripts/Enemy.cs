using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<float>, IKillable, IRewardable
{
    [SerializeField][Range(20f, 1000f)] float startHealth = 100;
    public float Health { get; set; }

    [SerializeField] ParticleSystem _bleeding;
    [field: SerializeField] public int Worth { get; set; } = 5;

    // TODO: Add death effect
    // public GameObject deathEffect;

    // TODO: Have a health bar 
    // [Header("Unity Stuff")]
    // [SerializeField] Image healthBar;

    bool isDead = false;

    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }

    private void OnEnable()
    {
        Health = startHealth;
        isDead = false;
        _bleeding.Stop();
    }

    public void Damage(float amount)
    {
        Health -= amount;

        // TODO: Decrease enemies health bar
        // healthBar.fillAmount = health / startHealth;

        if (Health <= 0 && !isDead)
        {
            RewardCoin(Worth);
            Kill();
        }
    }

    public void BleedingAnim(bool bleed)
    {
        if (bleed == true)
        {
            _bleeding.Play();
        }
    }

    public void Kill()
    {
        isDead = true;

        // TODO: Play death effect on enemy's death
        // GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        gameObject.SetActive(false);
    }

    public void RewardCoin(int amount)
    {
        if (bank == null) return;
        bank.Deposit(amount);
    }
}
