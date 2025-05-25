using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private TurretsSO turretData;

    private float timeUntilFire;

    [Header("Attributes")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int cost;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;

    [Header("Unity Setup Fields")]
    [SerializeField] private Transform turretRotate;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;


    public void Initialize(TurretsSO data)
    {
        turretData = data;
        target = data.target;
        range = data.range;
        cost = data.cost;
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            FindTarget();
            return;
        }

        RotateToTarget();

        if (!CheckTargetIsInRange())
            target = null;

        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / fireRate)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;

        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.Seek(target);
    }

    private void RotateToTarget()
    {
        float angle = Mathf.Atan2(target.position.y - turretRotate.position.y, target.position.x - turretRotate.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotate.rotation = Quaternion.RotateTowards(turretRotate.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
    /*
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }*/
}
