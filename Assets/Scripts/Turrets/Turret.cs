using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range;

    public string enemyTag = "Enemy";

    public void Initialize(TurretsSO data)
    {
        target = data.target;
        range = data.range;
    }

    private void Start ()
    {
        // Runs UpdateTarget method twice a second vs however many FPS the player is getting
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    } 

    // Turret tracks the nearest enemy based on the set range
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }

        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;
    }

    // Places a red sphere on the selected turret to show the range to the player
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }



}
