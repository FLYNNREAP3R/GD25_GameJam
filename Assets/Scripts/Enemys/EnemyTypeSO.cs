using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyType", menuName = "TD/Enemy Type")]
public class EnemyTypeSO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public int health;
    public float speed;
    public byte reward;
}
