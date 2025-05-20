using UnityEngine;

public class SpawnMinionsOnDeath : EnemyAbility
{
    public EnemyTypeSO minionType;
    public int minionCount = 2;
    public float spawnSpread = 1f;

    public override void OnDeath()
    {
        if (minionType == null || minionType.prefab == null) return;

        for (int i = 0; i < minionCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-spawnSpread, spawnSpread),0,Random.Range(-spawnSpread, spawnSpread));

            Enemy minion = EnemyPoolManager.Instance.GetEnemy(minionType);
            minion.transform.position = transform.position + offset;
            minion.transform.rotation = Quaternion.identity;
            minion.Initialize(minionType);
            minion.SetActualPath(enemy.currentWaypointIndex);
        }
    }


    public override void UpdateAbility() { }
}
