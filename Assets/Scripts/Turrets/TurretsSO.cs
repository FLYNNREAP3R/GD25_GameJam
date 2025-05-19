using UnityEngine;

[CreateAssetMenu(fileName = "TurretsSO", menuName = "TD/Turret Type")]
public class TurretsSO : ScriptableObject
{
    public string turretName;
    public GameObject prefab;
    public Transform target;
    public float range;
    public int cost; 
}
