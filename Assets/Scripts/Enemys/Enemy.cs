using UnityEditor.Playables;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;//Max Health
    private int actualHealth;
    private float speed;
    private int reward;

    private IEnemyAbility[] abilities;

    public void Initialize(EnemyTypeSO data)
    {
        health = data.health;
        speed = data.speed;
        reward = data.reward;

        abilities = GetComponents<IEnemyAbility>();
        foreach (var ability in abilities)
        {
            ability.Initialize(this);
        }
    }

    void Update()
    {
        foreach (var ability in abilities)
        {
            ability.UpdateAbility();
        }

        // Mover, seguir camino, etc.
    }

    public void TakeDamage(int dmg)
    {
        foreach(var ability in abilities) {
            if (ability is IModifyDamage modifier)
            {
                dmg = modifier.ModifyIncomingDamage(dmg);
            }
        }

        health -= dmg;
        if (health <= 0) Die();
    }
    public void Heal(int amount)
    {
        actualHealth += amount;
        actualHealth = Mathf.Min(actualHealth, health);
    }

    void Die()
    {
        foreach (var ability in abilities)
        {
            ability.OnDeath();
        }

        // Añadir recompensa, efectos, etc.
        Destroy(gameObject);
    }
}
