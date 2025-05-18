using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public WaveSO[] waves;

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
    }

    IEnumerator SpawnWave(WaveSO wave)
    {
        foreach (EnemySpawnData spawnData in wave.enemies)
        {
            for (int i = 0; i < spawnData.count; i++)
            {
                SpawnEnemy(spawnData.enemyType);
                yield return new WaitForSeconds(spawnData.delayBetweenSpawns);
            }
        }
    }

    void SpawnEnemy(EnemyTypeSO enemyType)
    {
        GameObject go = Instantiate(enemyType.prefab, spawnPoint.position, Quaternion.identity);
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.Initialize(enemyType);
    }
}
