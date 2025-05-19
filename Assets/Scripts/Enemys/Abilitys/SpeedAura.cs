using System.Collections.Generic;
using UnityEngine;

public class SpeedAura : EnemyAbility
{
    public float radius = 5f;
    public float speedMultiplier = 1.5f;

    private List<Enemy> affectedEnemies = new();

    public override void UpdateAbility()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        HashSet<Enemy> currentNearby = new();

        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out Enemy e) && e != enemy)
            {
                currentNearby.Add(e);
                if (!affectedEnemies.Contains(e))
                {
                    e.SetSpeedModifier(speedMultiplier);
                    affectedEnemies.Add(e);
                }
            }
        }

        // Remove enemies that left the aura
        for (int i = affectedEnemies.Count - 1; i >= 0; i--)
        {
            Enemy e = affectedEnemies[i];
            if (!currentNearby.Contains(e))
            {
                e.SetSpeedModifier(1f); // reset speed
                affectedEnemies.RemoveAt(i);
            }
        }
    }

    public override void OnDeath()
    {
        // Reset speed for all affected enemies
        foreach (var e in affectedEnemies)
        {
            e?.SetSpeedModifier(1f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
