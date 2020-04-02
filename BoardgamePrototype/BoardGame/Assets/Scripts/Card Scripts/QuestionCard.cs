using UnityEngine;
using TMPro;

public class QuestionCard : MonoBehaviour
{
    public TextMeshPro text;

    public void SetUpCard(Question c)
    {
        text.text = c.question;
    }
}
