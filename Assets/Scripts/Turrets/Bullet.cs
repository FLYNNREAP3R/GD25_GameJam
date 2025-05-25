using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private BulletSO bulletData;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [Header("Unity Setup Fields")] 
    [SerializeField] private Rigidbody2D rb;

    private Transform target;

    private void Initialize(BulletSO data)
    {
        bulletData = data;
        damage = data.damage;
        speed = data.speed;
    }
    void Update()
    {
        //if (target == null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //Vector3 dir = target.position - transform.position;
        //float distanceThisFrame = speed * Time.deltaTime;

        //if (dir.magnitude <= distanceThisFrame)
        //{
        //    SlowEnemy();
        //    Destroy(gameObject);
        //    return;
        //}
        //transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * speed;
        if(target.gameObject.activeInHierarchy == false)
        {
            Destroy(gameObject);
        }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == target.gameObject)
        {
            target.GetComponent<Enemy>().TakeDamage(bulletData.damage);
            Destroy(gameObject);
        }
    }

    void SlowEnemy()
    {
        target.GetComponent<Enemy>().SlowEnemy(0.7f, 3);
    }
}
