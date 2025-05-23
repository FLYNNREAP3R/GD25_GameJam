using UnityEngine;

[CreateAssetMenu(fileName = "BulletSO", menuName = "TD/Bullet Type")]
public class BulletSO : ScriptableObject
{
    private string bulletName;

    public GameObject prefab;
    public int damage;
    public float speed;
}
