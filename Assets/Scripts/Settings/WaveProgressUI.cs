using UnityEngine;
using UnityEngine.UI;

public class WaveProgressUI : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;

    private int totalEnemiesInWave = 1; // evitar divisi�n por cero
    private int spawnedEnemies = 0;

    public void Setup(int totalEnemies)
    {
        totalEnemiesInWave = Mathf.Max(1, totalEnemies); // previene divisi�n por cero
        spawnedEnemies = 0;
        UpdateSlider();
    }

    public void IncrementSpawned()
    {
        spawnedEnemies++;
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        progressSlider.value = (float)spawnedEnemies / totalEnemiesInWave;
    }
}
