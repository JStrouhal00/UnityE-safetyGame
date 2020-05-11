/*using System.Collections.Generic;
using UnityEngine;

public class CardMaster : MonoBehaviour
{

    public QuestionCard[] chanceReferences;

    private Queue<QuestionCard> chanceStack;

    public int shuffleMoves = 100;

    public GameObject cardPrefab;


    private QuestionCard currentChance;
    private GameObject activeCard;

    public UI ui;

    private Vector3 ViewVector;

    // Start is called before the first frame update
    void Start()
    {
        ViewVector = new Vector3(9f, 5f, -9f); // The vector location to make the cards readable

        // Copy the array
        QuestionCard[] toMix = new QuestionCard[chanceReferences.Length];
        chanceReferences.CopyTo(toMix, 0);

        // Shuffle the cards around
        QuestionCard temp;
        for (int i = 0; i < shuffleMoves; i++)
        {
            int idxA = Random.Range(0, toMix.Length);
            int idxB = Random.Range(0, toMix.Length);

            temp = toMix[idxA];
            toMix[idxA] = toMix[idxB];
            toMix[idxB] = temp;
        }

        // Initialise the "stack" of cards
        chanceStack = new Queue<QuestionCard>(toMix);
    }

    private GameObject CreateCard(QuestionCard c, Vector3 position)
    {
        GameObject cardDummy = Instantiate(cardPrefab, position, Quaternion.identity, null);
        Cards card = cardDummy.GetComponent<Cards>();
        if (card != null)
        {
            card.SetUpCard(c);
        }
        return cardDummy;
    }

    public bool CheckAnswer(bool answeredA)
    {
        if (currentChance == null)
        {
            Debug.Log("No Active Question");
            return false;
        }

        if (answeredA == currentChance.AIsRightAnswer)
            return true;
        else
            return false;

    }


    public void TriggerNextCard()
    {
        if (activeCard != null)
        {
            Destroy(activeCard);
        }
        if (chanceStack.Count == 0)
        {
            Debug.Log("No more questions!");
            ui.Show(false);
            return;
        }
        currentChance = chanceStack.Dequeue();
        activeCard = CreateCard(currentChance, ViewVector);
        ui.ShowQuestion(currentChance);
    }

}
*/