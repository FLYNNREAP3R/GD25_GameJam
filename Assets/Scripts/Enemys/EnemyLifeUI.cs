using UnityEngine;
using UnityEngine.UI;
public class EnemyLifeUI : MonoBehaviour
{
    [SerializeField] private Slider lifeBar;
    public void SetLife(int life)
    {
        lifeBar.maxValue = life;
        lifeBar.value = life;
    }
    public void UpdateLife(int life)
    {
        lifeBar.value = life;
    }
}
