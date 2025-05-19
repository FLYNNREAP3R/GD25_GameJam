using UnityEngine;

public class TeleportForward : EnemyAbility
{
    public float teleportInterval = 6f;
    public int waypointAdvance = 2; // saltar 2 waypoints
    private float timer;

    public override void UpdateAbility()
    {
        timer += Time.deltaTime;
        if (timer >= teleportInterval)
        {
            timer = 0f;
            TryTeleport();
        }
    }

    private void TryTeleport()
    {
        var e = enemy;

        int newIndex = Mathf.Min(e.currentWaypointIndex + waypointAdvance, e.path.Length - 1);
        e.currentWaypointIndex = newIndex;
        e.transform.position = e.path[newIndex].position;

        // Optional: efecto visual
        Debug.Log($"{e.name} teleported forward to waypoint {newIndex}");
    }

    public override void OnDeath()
    {
        // Nada especial
    }
}
