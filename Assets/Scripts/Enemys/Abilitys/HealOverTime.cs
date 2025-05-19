using UnityEngine;

public class HealOverTime : EnemyAbility
{
    public int healAmount = 1;
    public float healInterval = 1f;

    private float timer;

    public override void UpdateAbility()
    {
        timer += Time.deltaTime;
        if (timer >= healInterval)
        {
            timer = 0f;
            enemy.Heal(healAmount);
        }
    }

    public override void OnDeath()
    {
    }
}
