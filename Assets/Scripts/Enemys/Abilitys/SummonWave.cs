using UnityEngine;

public class SummonWave : EnemyAbility
{
    [System.Serializable]
    public class EnemySpawnData
    {
        public EnemyTypeSO enemyType;
        public int count = 1;
    }

    public EnemySpawnData[] enemiesToSummon;
    public float summonInterval = 10f;
    public float spawnRadius = 1.5f;
    public bool summonOnce = true;

    private bool hasSummoned = false;
    private float timer;

    public override void UpdateAbility()
    {
        if (summonOnce && hasSummoned) return;

        timer += Time.deltaTime;
        if (timer >= summonInterval)
        {
            timer = 0f;
            Summon();
            if (summonOnce) hasSummoned = true;
        }
    }

    private void Summon()
    {
        foreach (var data in enemiesToSummon)
        {
            for (int i = 0; i < data.count; i++)
            {
                Vector3 offset = Random.insideUnitSphere * spawnRadius;
                offset.y = 0f;

                Enemy newEnemy = EnemyPoolManager.Instance.GetEnemy(data.enemyType);
                newEnemy.transform.position = transform.position + offset;
                newEnemy.transform.rotation = Quaternion.identity;
                newEnemy.Initialize(data.enemyType);
                newEnemy.currentWaypointIndex = enemy.currentWaypointIndex; // Set the same waypoint index as the summoner
            }
        }

        Debug.Log($"{enemy.name} summoned reinforcements!");
    }


    public override void OnDeath() { }
}
