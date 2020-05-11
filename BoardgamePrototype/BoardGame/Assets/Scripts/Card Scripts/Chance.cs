using UnityEngine;

[CreateAssetMenu(menuName = "Chance Card")]

public class Chance : CardSO
{
    public string statement;

    public enum TypeOfChances {
        TurnMiss,
        Move
    }

    public TypeOfChances myChance;
    public int quantity;

    public void Activate() {
        if (myChance == TypeOfChances.TurnMiss) {
            GameManager.instance.GetActivePlayer().turnMiss = true;
            GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
        }
        else {
            GameManager.instance.GetActivePlayer().stone.MakeTurn(quantity);
        }
            
    }
}
