using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform target;

    public float speed = 70f;
    public BulletSO bulletData;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Debug.Log("Hello world");
            HitTarget();
            SlowEnemy();
            Destroy(gameObject);
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        target.GetComponent<Enemy>().TakeDamage(bulletData.damage);
    }

    void SlowEnemy()
    {
        target.GetComponent<Enemy>().SlowEnemy(0.7f, 3);
    }
}
