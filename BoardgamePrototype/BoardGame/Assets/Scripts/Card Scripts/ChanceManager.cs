using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceManager : MonoBehaviour
{

    public Chance[] chanceReferences;

    private Queue<Chance> chanceStack;

    public int shuffleMoves = 100;

    public GameObject cardPrefab;

    private Chance currentChance;
    private GameObject activeCard;

    private Vector3 viewVector;
    float countdown = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        viewVector = new Vector3(12.5f, 13.5f, 0.48f); // The vector location to make the cards readable

        // Copy the array
        Chance[] toMix = new Chance[chanceReferences.Length];
        chanceReferences.CopyTo(toMix, 0);

        // Shuffle the chance cards
        Chance temp;
        for (int i = 0; i < shuffleMoves; i++)
        {
            int idxA = Random.Range(0, toMix.Length);
            int idxB = Random.Range(0, toMix.Length);

            temp = toMix[idxA];
            toMix[idxA] = toMix[idxB];
            toMix[idxB] = temp;
        }

        // Initialise the stack
        chanceStack = new Queue<Chance>(toMix);

    }

    private GameObject CreateCard(Chance q, Vector3 position)
    {
        GameObject cardDummy = Instantiate(cardPrefab, position, Quaternion.identity, null);
        ChanceCard card = cardDummy.GetComponent<ChanceCard>();
        if (card != null)
        {
            card.SetUpCard(q);
        }
        return cardDummy;
    }

    public void TriggerNextChance()
    {
        if (activeCard != null)
        {
            Destroy(activeCard);
        }
        if (chanceStack.Count == 0)
        {
            Debug.Log("No more chance cards!");
            return;
        }
        currentChance = chanceStack.Dequeue();
        activeCard = CreateCard(currentChance, viewVector);
    }

        // Update is called once per frame
    void Update()
    {
        if (activeCard != null)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                countdown = 0;
                Destroy(activeCard);
            }
        }
    }
}
