/*// Note that this UI will seem quite empty until I come to incorporate the actual question cards

using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{

    public CardMaster cm;

    public TextMeshProUGUI answerA;
    public TextMeshProUGUI answerB;

    public void ShowQuestion(QuestionCard q)
    {
        answerA.text = q.answerA;
        answerB.text = q.answerB;
        Show(true);
    }

    public void Show(bool b)
    {
        gameObject.SetActive(b);
    }

    public void OnButtonAPress()
    {
        if (cm.CheckAnswer(true))
        {
            cm.TriggerNextCard();
        }
    }

    public void OnButtonBPress()
    {
        if (cm.CheckAnswer(false))
        {
            cm.TriggerNextCard();
        }
    }
}
*/