using UnityEngine;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public QuestionManager qm;

    public TextMeshProUGUI answerA;
    public TextMeshProUGUI answerB;

    void Start()
    {
        Show(false);
    }

    public void ShowQuestion(Question q)
    {
        answerA.transform.parent.gameObject.SetActive(true);
        answerB.transform.parent.gameObject.SetActive(true);
        answerA.text = q.answerA;
        answerB.text = q.answerB;
        Show(true);
    }

    public void ShowChance(Chance c) {
        answerA.transform.parent.gameObject.SetActive(false);
        answerB.transform.parent.gameObject.SetActive(false);
        Show(true);
    }

    public void Show(bool b)
    {
        gameObject.SetActive(b);
    }

    public void OnButtonAPress()
    {
        if (qm.CheckAnswer(true))
        {
            CorrectAnswer();

        }
        else
        {
            WrongAnswer();
        }
    }

    public void OnButtonBPress()
    {
        if (qm.CheckAnswer(false))
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    private void WrongAnswer()
    {
        Show(false);
        qm.CardShow(false);
        GameManager.instance.GetActivePlayer().AddScore(-5);
        GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
    }

    private void CorrectAnswer()
    {
        //qm.TriggerNextQuestion();
        Show(false);
        qm.CardShow(false);
        GameManager.instance.GetActivePlayer().AddScore(10);
        GameManager.instance.noSwitching = true;
        GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
        // Below will be the code that allows for player movement (which is whether they got it right or wrong)
        //GameManager.instance.state = GameManager.States.ROLL_DICE;
    }
}
