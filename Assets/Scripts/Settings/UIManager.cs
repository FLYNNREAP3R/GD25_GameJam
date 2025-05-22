using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _money;

    //Make Instance
    public static UIManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }
    public void UpdateMoney(int money)
    {
        _money.text = money.ToString();
    }
    public void Stop()
    {
        GameManager.instance.ChangeGameStatus(GameManager.GameState.Pause);
    }
    public void StartGame()
    {
        GameManager.instance.ChangeGameStatus(GameManager.GameState.Play);
    }
    public void ShowFinalPanel()
    {

    }
}
