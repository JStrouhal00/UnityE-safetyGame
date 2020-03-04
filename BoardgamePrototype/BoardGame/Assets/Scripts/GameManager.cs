using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dice dice;

    int activePlayer;
    int diceNumber;

    [System.Serializable]

    public class Player
    {
        public string PlayerName;
        public Stone stone;

        public enum PlayerTypes
        {
            CPU,
            HUMAN
        }
        public PlayerTypes playertype;


    }

    public List<Player> playerList = new List<Player>();

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
                        StartCoroutine(RollDiceDelay());
                        state = States.WAITING;

                    }
                    break;
                case States.SWITCH_PLAYER:
                    {
                        activePlayer++;
                        activePlayer %= playerList.Count;

                        state = States.ROLL_DICE;

                    }
                    break;
            }

        }
    }

    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(2);
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
    

        
}
