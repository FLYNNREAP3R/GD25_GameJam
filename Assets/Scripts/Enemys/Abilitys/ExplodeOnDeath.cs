using UnityEngine;

public class ExplodeOnDeath : EnemyAbility
{
    public float radius = 2f;
    public int damage = 1;

    public override void OnDeath()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Tower"))
            {
                // Asume que las torres tienen un método TakeDamage
                //hit.GetComponent<Tower>()?.TakeDamage(damage);
            }
        }

        // Aquí podrías añadir un efecto visual de explosión
        Debug.Log($"{gameObject.name} explotó al morir");
    }

    public override void UpdateAbility()
    {
        // Esta habilidad no hace nada en Update
    }
}
