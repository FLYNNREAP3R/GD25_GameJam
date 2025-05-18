using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private float speed;
    private int reward;

    public void Initialize(EnemyTypeSO data)
    {
        health = data.health;
        speed = data.speed;
        reward = data.reward;
    }

    /*
     private void Update(){
        //USE speed


        //Check is enemy is on nexus
        //if (Vector3.Distance(transform.position, nexus.position) < 0.1f)
    }
     */

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }

    void Die()
    {
        //GAMEMANAGER OR PLAYERMANAGER add REWARD
        Destroy(gameObject);
    }
}
