using UnityEngine;
using UnityEngine.UI;
public class Nexus : MonoBehaviour
{
    public int health; // Vida máxima
    private int actualHealth; // Vida actual
    [SerializeField] private Slider _life;
    //instance
    public static Nexus instance;
    private void Start()
    {
        actualHealth = health;
        _life.maxValue = health;
        _life.value = health;
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;

    }
    public void TakeDamage(int dmg)
    {
        actualHealth -= dmg;
        _life.value = actualHealth;
        if (actualHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Lógica para la muerte del Nexus
        Debug.Log("Nexus destroyed!");
        //Instance Particle System
    }
}
