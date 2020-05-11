using UnityEngine;
using TMPro;

public class ChanceCard : MonoBehaviour
{
    public TextMeshPro text;
    public Chance myChance;

    public void SetUpCard(Chance c)
    {
        myChance = c;
        text.text = c.statement;
    }
}
