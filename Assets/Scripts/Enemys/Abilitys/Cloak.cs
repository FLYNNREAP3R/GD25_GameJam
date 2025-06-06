using UnityEngine;

public class Cloak : EnemyAbility
{
    private bool isCloaked = true;
    private Renderer[] renderers;

    public override void Initialize(Enemy _enemy)
    {
        base.Initialize(_enemy);
        isCloaked = true;
        renderers = GetComponentsInChildren<Renderer>();
        SetCloak(true);
        Invoke("Reveal", 5f); // Desvela despu�s de 5 segundos
    }

    public override void UpdateAbility() { }

    public void Reveal()
    {
        if (isCloaked)
        {
            isCloaked = false;
            SetCloak(false);
        }
    }

    private void SetCloak(bool active)
    {
        foreach (var r in renderers)
        {
            r.enabled = !active; // Oculta el render si cloaked
        }
        // Layer or tag to identify the enemy
        gameObject.tag = active ? "CloakedEnemy" : "Enemy";
        gameObject.layer = active ? LayerMask.NameToLayer("CloakedEnemy") : LayerMask.NameToLayer("Enemy");
    }
}
