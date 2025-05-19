using UnityEngine;

public class HealNearby : EnemyAbility
{
    public float healRadius = 4f;
    public int healAmount = 2;
    public float healInterval = 2f;

    private float timer = 0f;

    public override void UpdateAbility()
    {
        timer += Time.deltaTime;
        if (timer >= healInterval)
        {
            timer = 0f;
            HealEnemiesInRange();
        }
    }

    private void HealEnemiesInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, healRadius);
        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out Enemy e) && e != enemy)
            {
                e.Heal(healAmount);
            }
        }
    }

    public override void OnDeath(){ }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
}
