using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dice dice;

    int activePlayer;
    int diceNumber;

    public Camera camera;
    public Stone[] stones;
    private int stonesUsed = 0;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private Text winText;

    [System.Serializable]
    public class Player
    {
        public string PlayerName;
        public Stone stone;

        public int score = 0;
        public bool turnMiss = false;

        public enum PlayerTypes
        {
            CPU,
            HUMAN
        }
        public PlayerTypes playertype;

        public Player(string playerName, Stone stone, PlayerTypes playertype)
        {
            PlayerName = playerName;
            this.stone = stone;
            this.score = 0;
            this.playertype = playertype;
        }

        public void AddScore(int score)
        {
            this.score = Mathf.Max(this.score + score, 0);
        }

    }

    public List<Player> playerList = new List<Player>();
    public bool gameStarted = false;
    private bool switched = false;
    public bool noSwitching = false;

    public enum States
    {
        WAITING,
        ROLL_DICE,
        SWITCH_PLAYER,
    }

    public States state;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(gameStarted == false) {
            return;
        }

        /*
        if(playerList[activePlayer].playertype == Player.PlayerTypes.CPU)
        {
            switch (state)
            {
                case States.WAITING:
                    {
                        //IDLE
                    }
                    break;
                case States.ROLL_DICE:
                    {
                        if (GetActivePlayer().turnMiss) {
                            state = States.SWITCH_PLAYER;
                            GetActivePlayer().turnMiss = false;
                            return;
                        }

                        StartCoroutine(RollDiceDelay());
                        state = States.WAITING;

                    }
                    break;
                case States.SWITCH_PLAYER:
                    {
                        if (!switched && !noSwitching) {
                            activePlayer++;
                            activePlayer %= playerList.Count;
                            switched = true;
                        }
                        

                        //state = States.ROLL_DICE;

                    }
                    break;
            }

        }
        */

        switch (state)
        {
            case States.WAITING:
                {
                    //IDLE
                }
                break;
            case States.ROLL_DICE:
                {
                    if (GetActivePlayer().turnMiss) {
                        state = States.SWITCH_PLAYER;
                        GetActivePlayer().turnMiss = false;
                        return;
                    }

                    StartCoroutine(RollDiceDelay());
                    state = States.WAITING;

                }
                break;
            case States.SWITCH_PLAYER:
                {
                    if (!switched && !noSwitching) {
                        activePlayer++;
                        activePlayer %= playerList.Count;
                        switched = true;
                    }
                    

                    //state = States.ROLL_DICE;

                }
                break;
        }
    }

    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(0.2f);
        //diceNumber = Random.Range(1, 7);

        //ROLL THE PHYSICAL DICE
        dice.RollDice();

        
        
    }

    public void RolledNumber(int _diceNumber)//CALLED FROM THE DICE
    {
        diceNumber = _diceNumber;
        //Make a Turn
        playerList[activePlayer].stone.MakeTurn(diceNumber);
    }

    private IEnumerator SwitchStates(States state, float delay=0) {
        yield return new WaitForSeconds(delay);

        this.state = state;
    }
    
    public Player GetActivePlayer()
    {
        return playerList[activePlayer];
    }
    
    public void ResetPlayers()
    {
        playerList.Clear();
    }

    public void AddPlayer(string name)
    {
        playerList.Add(new Player(name, GetFreeStone(), Player.PlayerTypes.HUMAN));
    }

    private Stone GetFreeStone()
    {
        Stone s = stones[stonesUsed];
        stonesUsed++;
        return s;
    }

    public void RollDiceButton() {
        if (state != States.SWITCH_PLAYER) return;

        state = States.ROLL_DICE;
        switched = false;
        noSwitching = false;
    }

    public void WinTrigger() {
        gameStarted = false;

        Player winner = playerList[0].score > playerList[1].score ? playerList[0] : playerList[1];
        string winnerText = winner.PlayerName + " wins with " + winner.score + " points!";
        if (playerList[0].score == playerList[1].score) {
            winnerText = "It's a tie!";
        }

        winText.text = winnerText;
        winScreen.SetActive(true);

        Leaderboard.Record(playerList[0].PlayerName, playerList[0].score);
        Leaderboard.Record(playerList[1].PlayerName, playerList[1].score);
    }

    public void GoToMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
