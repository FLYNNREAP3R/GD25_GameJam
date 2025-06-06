using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    private Dictionary<EnemyTypeSO, Queue<Enemy>> poolDictionary = new();
    public Transform enemyContainer;  // El contenedor donde se agruparán los enemigos
    public bool OnAllEnemiesCleared;
    private int activeEnemies = 0;
    private bool allWavesFinished = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Obtener enemigo del pool
    public Enemy GetEnemy(EnemyTypeSO type)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            poolDictionary[type] = new Queue<Enemy>();
        }
        activeEnemies++;
        if (poolDictionary[type].Count > 0)
        {
            Enemy enemy = poolDictionary[type].Dequeue();
            enemy.gameObject.SetActive(true);
            enemy.transform.SetParent(enemyContainer);  // Asignar el contenedor
            return enemy;
        }
        else
        {
            // Si no hay enemigos disponibles en el pool, instanciar uno nuevo
            GameObject go = Instantiate(type.prefab);
            Enemy newEnemy = go.GetComponent<Enemy>();
            newEnemy.transform.SetParent(enemyContainer);  // Asignar el contenedor
            return newEnemy;
        }
    }

    // Devolver un enemigo al pool
    public void ReturnToPool(Enemy enemy, EnemyTypeSO type)
    {
        enemy.gameObject.SetActive(false);
        activeEnemies--;
        enemy.transform.SetParent(enemyContainer);  // Asignar el contenedor cuando se desactiva
        poolDictionary[type].Enqueue(enemy);
        if (activeEnemies <= 0 && allWavesFinished)
        {
            Debug.Log("ˇVictoria detectada desde el Pool!");
            GameManager.instance.GameWin();
        }
    }
    public void SetAllWavesFinished()
    {
        allWavesFinished = true;
    }
}
