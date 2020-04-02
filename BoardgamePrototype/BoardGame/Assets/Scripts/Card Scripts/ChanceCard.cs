using UnityEngine;
using TMPro;

public class ChanceCard : MonoBehaviour
{
    public TextMeshPro text;

    public void SetUpCard(Chance c)
    {
        text.text = c.statement;
    }
}
