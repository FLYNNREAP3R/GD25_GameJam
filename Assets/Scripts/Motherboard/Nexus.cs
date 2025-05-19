using UnityEngine;
using UnityEngine.UI;
public class Nexus : MonoBehaviour
{
    public int health; // Vida máxima
    private int actualHealth; // Vida actual
    [SerializeField] private Slider _life;
    private void Start()
    {
        actualHealth = health;
        _life.maxValue = health;
        _life.value = health;

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
