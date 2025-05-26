using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private BulletSO bulletData;
    private float speed;
    private int damage;

    [Header("Unity Setup Fields")] 
    [SerializeField] private Rigidbody2D rb;

    private Transform target;
    [SerializeField] private bool slowEnemy = false;

    public void Initialize()
    {
        damage = bulletData.damage;
        speed = bulletData.speed;
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
            target.GetComponent<Enemy>().TakeDamage(damage);
            if (slowEnemy)
            {
                SlowEnemy();
            }
            Destroy(gameObject);
        }
    }

    void SlowEnemy()
    {
        target.GetComponent<Enemy>().SlowEnemy(0.7f, 3);
    }
}
