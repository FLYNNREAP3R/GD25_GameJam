using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public EnemyTypeSO enemyType;
    public int count;
    public float delayBetweenSpawns = 0.5f;
}

[CreateAssetMenu(fileName = "NewWave", menuName = "TD/Wave")]
public class WaveSO : ScriptableObject
{
    public EnemySpawnData[] enemies;
}
