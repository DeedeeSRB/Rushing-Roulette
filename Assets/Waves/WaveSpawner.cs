using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    [SerializeField] Wave[] waves;

    [Space][SerializeField] Transform spawnPoint;

    [Header("Countdown")]
    [SerializeField][Range(0.1f, 5f)] float initialCountdown = 2f;
    [ReadOnly] public float curCountdown;

    //public GameManager gameManager;   

    [Header("Wave")]
    [SerializeField] TextMeshProUGUI waveCountdownText;
    [SerializeField] TextMeshProUGUI waveCounterText;
    int waveIndex = 0;
    [ReadOnly] public bool isNextWaveReady = true;

    [Space][SerializeField] GameObject enemyObjectPool;
    List<GameObject> enemyPool = new List<GameObject>();
    bool loadingPool = false;
    bool finishedPool = false;

    void Start()
    {
        curCountdown = initialCountdown;
        waveCounterText.text = "Wave: 1";
    }

    void Update()
    {
        if (EnemiesAlive > 0 || !isNextWaveReady) return;
        if (waveIndex == waves.Length)
        {
            //gameManager.WinLevel();
            this.enabled = false;
            return;
        }

        ResetPool();
        if (curCountdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            return;
        }

        curCountdown -= Time.deltaTime;
        curCountdown = Mathf.Clamp(curCountdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", curCountdown);
    }

    IEnumerator SpawnWave()
    {
        //PlayerStats.Rounds++;
        isNextWaveReady = false;
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.subwaves.Length; i++)
        {
            Subwave subwave = wave.subwaves[i];

            for (int j = 0; j < subwave.count; j++)
            {
                SpawnEnemy(subwave.enemy);
                EnemiesAlive++;
                yield return new WaitForSeconds(1f / subwave.rate);
            }

            if (i >= 0 && i != wave.subwaves.Length - 1)
                yield return new WaitForSeconds(subwave.waitTime);
        }

        waveIndex++;
        isNextWaveReady = true;
        curCountdown = wave.waitTime;
        finishedPool = false;

        waveCounterText.text = "Wave: " + (waveIndex + 1);

    }

    void ResetPool()
    {
        if (!loadingPool && !finishedPool && waveIndex < waves.Length)
        {
            finishedPool = true;
            loadingPool = true;
            EmptyPool();
            PopulatePool();
            loadingPool = false;
        }
    }

    void EmptyPool()
    {
        if (enemyPool == null) return;
        for (int i = 0; i < enemyPool.Count; i++)
        {
            Destroy(enemyPool[i]);
        }
        enemyPool.Clear();
    }

    void PopulatePool()
    {
        foreach (Subwave subwave in waves[waveIndex].subwaves)
        {
            for (int i = 0; i < subwave.count; i++)
            {
                GameObject enemy = subwave.enemy;
                enemy.SetActive(false);
                enemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                enemy.transform.parent = enemyObjectPool.transform;
                enemyPool.Add(enemy);
            }
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        EnableObjectInPool();
        //enemy.SetActive(true);
        //Instantiate(enemy, enemyObjectPool.transform);
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (enemyPool[i].activeInHierarchy == false)
            {
                enemyPool[i].SetActive(true);
                return;
            }
        }
    }

}
