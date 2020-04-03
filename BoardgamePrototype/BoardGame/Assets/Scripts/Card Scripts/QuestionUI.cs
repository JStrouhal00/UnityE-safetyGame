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
        if (qm.CheckAnswer(true))
        {
            //qm.TriggerNextQuestion();
            Show(false);
            qm.CardShow(false);

            // Below will be the code that allows for player movement (which is whether they got it right or wrong)
        }
    }

    public void OnButtonBPress()
    {
        if (qm.CheckAnswer(false))
        {
            //qm.TriggerNextQuestion();
            Show(false);
            qm.CardShow(false);

            // Below will be the code that allows for player movement (which is whether they got it right or wrong)
        }
    }
}
