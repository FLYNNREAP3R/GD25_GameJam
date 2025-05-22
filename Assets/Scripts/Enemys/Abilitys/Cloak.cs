using UnityEngine;

public class Cloak : EnemyAbility
{
    public bool isCloaked = true;
    private Renderer[] renderers;

    public override void Initialize(Enemy _enemy)
    {
        base.Initialize(_enemy);
        renderers = GetComponentsInChildren<Renderer>();
        SetCloak(true);
        Invoke("Reveal", 5f); // Desvela después de 5 segundos
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

        // Marcar el estado para las torres
        // Layer or tag to identify the enemy
        gameObject.tag = active ? "CloakedEnemy" : "Enemy";
        gameObject.layer = active ? LayerMask.NameToLayer("CloakedEnemy") : LayerMask.NameToLayer("Enemy");
    }
}
