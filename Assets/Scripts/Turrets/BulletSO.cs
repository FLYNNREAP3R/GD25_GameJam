using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "TD/Bullet Type")]
public class BulletSO : ScriptableObject
{
    private string name;
    public GameObject prefab;
    public int damage;
}
