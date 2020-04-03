using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public Question[] questionsReferences;

    private Queue<Question> questionStack;

    public int shuffleMoves = 100;

    public GameObject cardPrefab;

    private Question currentQuestion;
    private GameObject activeCard;

    public QuestionUI ui;

    private Vector3 ViewVector;

    // Start is called before the first frame update
    void Start()
    {
        ViewVector = new Vector3(12.5f, 13.5f, 0.48f); // The vector location to make the cards readable

        //Copy the array
        Question[] toMix = new Question[questionsReferences.Length];
        questionsReferences.CopyTo(toMix, 0);

        //Suffle the questions
        Question temp;
        for (int i = 0; i < shuffleMoves; i++)
        {
            int idxA = Random.Range(0, toMix.Length);
            int idxB = Random.Range(0, toMix.Length);

            temp = toMix[idxA];
            toMix[idxA] = toMix[idxB];
            toMix[idxB] = temp;
        }

        //Initialize the question stack
        questionStack = new Queue<Question>(toMix);

        //TriggerNextQuestion();
    }

    private GameObject CreateCard(Question q, Vector3 position)
    {
        GameObject cardDummy = Instantiate(cardPrefab, position, Quaternion.identity, null);
        QuestionCard card = cardDummy.GetComponent<QuestionCard>();
        if (card != null)
        {
            card.SetUpCard(q);
        }
        return cardDummy;
    }

    public bool CheckAnswer(bool answeredA)
    {
        if (currentQuestion == null)
        {
            Debug.Log("No Active Question");
            return false;
        }

        if (answeredA == currentQuestion.AIsRight)
            return true;
        else
            return false;

    }

    public void CardShow(bool b)
    {
        activeCard.SetActive(b);
    }

    public void TriggerNextQuestion()
    {
        if (activeCard != null)
        {
            Destroy(activeCard);
        }
        if (questionStack.Count == 0)
        {
            Debug.Log("No more questions!");
            ui.Show(false);
            return;
        }
        currentQuestion = questionStack.Dequeue();
        activeCard = CreateCard(currentQuestion, ViewVector);
        ui.ShowQuestion(currentQuestion);
    }
}
