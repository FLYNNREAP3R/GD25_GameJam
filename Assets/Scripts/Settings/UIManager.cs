using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private GameObject _defeatPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button[] _turretsButton;
    [SerializeField] private TurretsSO[] _turrets;
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
        //Update shop buttons (interactables)
        for (int i = 0; i < _turretsButton.Length; i++)
        {
            if (_turrets[i].cost > money)
            {
                _turretsButton[i].interactable = false;
            }
            else
            {
                _turretsButton[i].interactable = true;
            }
        }
    }

    public void SetSelected()
    {

    }
    public void Stop()
    {
        GameManager.instance.ChangeGameStatus(GameManager.GameState.Pause);
    }
    public void StartGame()
    {
        GameManager.instance.ChangeGameStatus(GameManager.GameState.Play);
    }
    public void ShowFinalPanel(bool winLose)
    {
        if(winLose)
        {
            _winPanel.SetActive(true);
        }
        else
        {
            _defeatPanel.SetActive(true);
        }
    }
}
