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
                // Asume que las torres tienen un m�todo TakeDamage
                //hit.GetComponent<Tower>()?.TakeDamage(damage);
            }
        }

        // Aqu� podr�as a�adir un efecto visual de explosi�n
        Debug.Log($"{gameObject.name} explot� al morir");
    }

    public override void UpdateAbility()
    {
        // Esta habilidad no hace nada en Update
    }
}
