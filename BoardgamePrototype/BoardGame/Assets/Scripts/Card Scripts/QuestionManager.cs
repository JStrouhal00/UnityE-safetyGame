using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public Question[] questionsReferences;
    public Chance[] chanceReferences;

    private Queue<Question> questionStack;
    private Queue<Chance> chanceStack;

    public int shuffleMoves = 100;

    public GameObject cardPrefab;
    public GameObject chancePrefab;

    private Question currentQuestion;
    private GameObject activeCard;

    private Chance currentChance;

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
        chanceStack = new Queue<Chance>(chanceReferences);

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

    private GameObject CreateChanceCard(Chance c, Vector3 position) {
        GameObject cardDummy = Instantiate(chancePrefab, position, Quaternion.identity, null);
        ChanceCard card = cardDummy.GetComponent<ChanceCard>();
        if (card != null)
        {
            card.SetUpCard(c);
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
        if(b == false)
        {
            if(lastAnimator != null)
            {
                lastAnimator.ReturnToOriginalPosition();
                lastAnimator = null;
            }
        }
    }

    private QuestionMarkAnimator lastAnimator;

    public void TriggerNextQuestion(QuestionMarkAnimator anim)
    {
        lastAnimator = anim;
        TriggerNextQuestion();
    }

    public void TriggerNextQuestion()
    {
        if (activeCard != null)
        {
            Destroy(activeCard);
        }
        if (questionStack.Count == 0 && chanceStack.Count == 0)
        {
            Debug.Log("No more questions!");
            ui.Show(false);
            return;
        }
        if (Random.value < 0.5) {
            currentQuestion = questionStack.Dequeue();
            activeCard = CreateCard(currentQuestion, ViewVector);
            ui.ShowQuestion(currentQuestion);
        }
        else
        {
            currentChance = chanceStack.Dequeue();
            activeCard = CreateChanceCard(currentChance, ViewVector);
            ui.ShowChance(currentChance);

            StartCoroutine(ActivateChanceCard(activeCard, 2f));
        }
        
    }

    IEnumerator ActivateChanceCard(GameObject go, float delay=0) {
        yield return new WaitForSeconds(delay);

        ui.Show(false);
        CardShow(false);
        go.GetComponent<ChanceCard>().myChance.Activate();
    }
}
