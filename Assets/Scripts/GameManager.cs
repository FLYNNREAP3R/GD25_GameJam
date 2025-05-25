using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Make Instance
    public static GameManager instance;
    public enum GameState { Start, Play, Pause, End }//Game State
    [SerializeField]private GameState currentGameState = GameState.Start;
    private int score = 0;
    private bool winLose = false; //Win or Lose
    public int money = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
        ChangeGameStatus(GameState.Start);
        Debug.Log("TimeScale: " + Time.timeScale);
    }

    public void EnemyDeath(int reward)
    {
        //Increase Score
        score++;
        //Increase Money
        money += reward;
        //Update UI
        UIManager.instance.UpdateMoney(money);
        UIManager.instance.UpdateScore(score);
    }

    public bool SpendMoney(int amount)
    {
        if (amount <= money)
        {
            money -= amount;
            UIManager.instance.UpdateMoney(money);
            return true;
        }
        else
        {
            Debug.Log("You do not have enough money to purchase this item!");
            return false;
        }
    }

    public void ChangeGameStatus(GameState newGameState)
    {
        currentGameState = newGameState;
        switch (currentGameState)
        {
            case GameState.Start:
                //Start Game
                Time.timeScale = 1;
                score = 0;
                money = 10;
                UIManager.instance.UpdateMoney(money);
                UIManager.instance.UpdateScore(score);
                break;
            case GameState.Play:
                //Play Game
                Time.timeScale = 1;
                break;
            case GameState.Pause:
                //Pause Game
                Time.timeScale = 0;
                break;
            case GameState.End:
                //End Game
                Time.timeScale = 0;
                UIManager.instance.ShowFinalPanel(winLose);
                break;
        }
    }

    public void GameOver()
    {
        winLose = false; // Set to false for Game Over
        ChangeGameStatus(GameState.End);
        Debug.Log("Game Over");
    }

    public void GameWin()
    {
        winLose = true; // Set to true for Game Win
        ChangeGameStatus(GameState.End);
        Debug.Log("You Win!");
    }
}
