using UnityEngine;
using System.Collections;
using Unity.Collections;

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
    private bool isSlowed;
    private void Start()
    {
        if (path == null || path.Length == 0)
        {
            path = PathManager.Instance.waypoints;
        }
    }

    public void Initialize(EnemyTypeSO data)
    {
        currentWaypointIndex = 0;
        enemyTypeSO = data;
        health = data.health;
        actualHealth = health; // Set actual health to max health
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
        MoveAlongPath();
    }
    void MoveAlongPath()
    {
        if (currentWaypointIndex >= path.Length)
        {
            Debug.Log("Enemy reached the end of the path");
            Nexus.instance.TakeDamage(1);
            gameObject.SetActive(false);
            return;
        }
        Transform target = path[currentWaypointIndex];
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * GetCurrentSpeed() * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
    #region Speed
    public float GetCurrentSpeed() => speed * speedModifier;

    public void SetSpeedModifier(float multiplier)
    {
        speedModifier += multiplier;
    }

    public void SlowEnemy(float slowSpeed, float timeSlowed)
    {
        if (!isAlive || isSlowed == true) return;

        StartCoroutine(SlowEnemyTimer(slowSpeed, timeSlowed));
    }

    private IEnumerator SlowEnemyTimer(float slowSpeed, float timeSlowed)
    {
        isSlowed = true;
        speed = speed - slowSpeed;

        yield return new WaitForSeconds(timeSlowed);

        speed += slowSpeed;
        isSlowed = false;
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

        // Modificar da�o seg�n habilidades
        foreach (var ability in abilities)
        {
            if (ability is IModifyDamage modifier)
            {
                dmg = modifier.ModifyIncomingDamage(dmg);
            }
        }

        actualHealth -= dmg;
        enemyLifeUI.UpdateLife(actualHealth);
        if (actualHealth <= 0)
        {
            Die();
        }
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

        GameManager.instance.EnemyDeath(reward);
        //Pool, disable
        EnemyPoolManager.Instance.ReturnToPool(this, enemyTypeSO);
        //gameObject.SetActive(false);
    }
    //For enemys instanced from another enemy
    public void SetActualPath(int currentWaypoint)
    {
        currentWaypointIndex = currentWaypoint;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        if (other.CompareTag("CPU"))
        {
            Debug.Log("CPU Hit");
            Nexus nexus = other.GetComponent<Nexus>();
            if (nexus != null)
            {
                Debug.Log("Give Hit to CPU");
                nexus.TakeDamage(1);
                // A�adir l�gica para eliminar el enemigo
                gameObject.SetActive(false);
            }
        }
    }
}
