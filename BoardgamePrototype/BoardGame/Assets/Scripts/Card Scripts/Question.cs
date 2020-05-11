using UnityEngine;

[CreateAssetMenu(menuName = "Question Card")]

public class Question : CardSO
{
    public string question;
    public string answerA;
    public string answerB;
    public bool AIsRight;
}
