using UnityEngine;

public class Armor : EnemyAbility, IModifyDamage
{
    public int flatReduction = 1;
    public bool usePercentage = false;
    [Range(0f, 1f)] public float percentReduction = 0.25f; // 25% reducción

    public int ModifyIncomingDamage(int baseDamage)
    {
        if (usePercentage)
        {
            int reduced = Mathf.FloorToInt(baseDamage * (1f - percentReduction));
            return Mathf.Max(reduced, 1); // mínimo 1 de daño
        }
        else
        {
            int reduced = baseDamage - flatReduction;
            return Mathf.Max(reduced, 1); // mínimo 1 de daño
        }
    }

    public override void UpdateAbility() { }
}
