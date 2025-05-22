using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    public float range;
    public float fireRate = 1f;
    private float fireCountdown = 0f;


    [Header("Unity Setup Fields")]

    public float turnSpeed = 10f;
    public int cost;
    public Transform turretRotate;
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;

    public void Initialize(TurretsSO data)
    {
        target = data.target;
        range = data.range;
        cost = data.cost;

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

        // Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        turretRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

        void Shoot()
        {
           GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
           Bullet bullet = bulletGO.GetComponent<Bullet>();

           if (bullet != null)
           {
               bullet.Seek(target);
           }
        }

    }

    // Places a red sphere on the selected turret to show the range to the player
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }



}
