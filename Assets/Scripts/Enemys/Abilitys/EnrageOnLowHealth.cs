using UnityEngine;

public class EnrageOnLowHealth : EnemyAbility
{
    [Range(0f, 1f)]
    public float healthThreshold = 0.3f; // 30%
    public float speedMultiplier = 2f;
    public bool enraged = false;

    public override void UpdateAbility()
    {
        if (!enraged && enemy.GetHealth() <= enemy.GetMaxHealth() * healthThreshold)
        {
            ActivateEnrage();
        }
    }

    private void ActivateEnrage()
    {
        enraged = true;
        enemy.SetSpeedModifier(speedMultiplier);

        //Color
        foreach (var r in enemy.GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.red;
        }

        // Instantiate(enrageEffectPrefab, transform.position, Quaternion.identity, transform);
    }


    public override void OnDeath()
    {
        // Nada especial
    }
}
