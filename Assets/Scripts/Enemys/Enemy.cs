using UnityEditor.Playables;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Transform[] path; // lista de puntos del camino
    public int currentWaypointIndex = 0;
    private int health;//Max Health
    private int actualHealth;
    private float speed;
    private int reward;
    private float speedModifier = 1f;
    private IEnemyAbility[] abilities;
    private EnemyLifeUI enemyLifeUI;
    private bool isAlive = true;
    private EnemyTypeSO enemyTypeSO;
    private void Start()
    {
        if (path == null || path.Length == 0)
        {
            path = PathManager.Instance.waypoints;
        }
        // Inicializar la vida
        actualHealth = health;
        enemyLifeUI = GetComponentInChildren<EnemyLifeUI>();
        enemyLifeUI.SetLife(health);
        // Inicializar habilidades
        foreach (var ability in abilities)
        {
            ability.Initialize(this);
        }
    }


    public void Initialize(EnemyTypeSO data)
    {
        enemyTypeSO = data;
        health = data.health;
        speed = data.speed;
        reward = data.reward;
        isAlive = true;
        abilities = GetComponents<IEnemyAbility>();
        enemyLifeUI = GetComponentInChildren<EnemyLifeUI>();
        enemyLifeUI.SetLife(health);
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
        //Move forward 2d
        MoveAlongPath();
    }
    void MoveAlongPath()
    {
        if (currentWaypointIndex >= path.Length) return;

        Transform target = path[currentWaypointIndex];
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * GetCurrentSpeed() * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentWaypointIndex++;
        }
    }
    #region Speed
    public float GetCurrentSpeed() => speed * speedModifier;

    public void SetSpeedModifier(float multiplier)
    {
        speedModifier = multiplier;
    }
    #endregion
    #region Life
    public void TakeDamage(int dmg)
    {
        if(!isAlive) return;
        // Revelar si tiene Cloak
        foreach (var ability in abilities)
        {
            if (ability is Cloak cloak)
            {
                cloak.Reveal();
            }
        }

        // Modificar daño según habilidades
        foreach (var ability in abilities)
        {
            if (ability is IModifyDamage modifier)
            {
                dmg = modifier.ModifyIncomingDamage(dmg);
            }
        }

        actualHealth -= dmg;
        enemyLifeUI.UpdateLife(actualHealth);
        if (actualHealth <= 0) Die();
    }

    public void Heal(int amount)
    {
        actualHealth += amount;
        actualHealth = Mathf.Min(actualHealth, health);
        enemyLifeUI.UpdateLife(actualHealth);
    }
    public int GetHealth()
    {
        return actualHealth;
    }
    public int GetMaxHealth()
    {
        return health;
    }
    #endregion
    void Die()
    {
        isAlive = false;
        foreach (var ability in abilities)
        {
            ability.OnDeath();
        }

        // Añadir recompensa, efectos, etc.
        //Pool, disable
        EnemyPoolManager.Instance.ReturnToPool(this, enemyTypeSO);
        //gameObject.SetActive(false);
    }
    //For enemys instanced from another enemy
    public void SetActualPath(int currentWaypoint)
    {
        currentWaypointIndex = currentWaypoint;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CPU"))
        {
            Nexus nexus = other.GetComponent<Nexus>();
            if (nexus != null)
            {
                nexus.TakeDamage(1);
                // Añadir lógica para eliminar el enemigo
                gameObject.SetActive(false);
            }
        }
    }
}
