using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public WaveSO[] waves;
    [SerializeField] private WaveProgressUI waveProgressUI;
    public float timeBetweenWaves = 5f;
    private int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnWavesRoutine());
    }

    IEnumerator SpawnWavesRoutine()
    {
        while (currentWaveIndex < waves.Length)
        {
            WaveSO wave = waves[currentWaveIndex];
            yield return StartCoroutine(SpawnWave(wave));
            currentWaveIndex++;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        EnemyPoolManager.Instance.SetAllWavesFinished();
    }

    IEnumerator SpawnWave(WaveSO wave)
    {
        int totalEnemies = 0;

        foreach (var data in wave.enemies)
            totalEnemies += data.count;

        waveProgressUI.Setup(totalEnemies);

        foreach (EnemySpawnData spawnData in wave.enemies)
        {
            for (int i = 0; i < spawnData.count; i++)
            {
                SpawnEnemy(spawnData.enemyType);
                waveProgressUI.IncrementSpawned();
                yield return new WaitForSeconds(spawnData.delayBetweenSpawns);
            }
        }
    }

    void SpawnEnemy(EnemyTypeSO enemyType)
    {
        Enemy enemy = EnemyPoolManager.Instance.GetEnemy(enemyType);
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = Quaternion.identity;
        enemy.Initialize(enemyType);
    }
}
