using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMaster : MonoBehaviour
{

    public Transform obj;
    private Vector3 ViewVector;
    private Cards tempGO;

    static public List<GameObject> objs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Add Cards to the list
        foreach (GameObject cardEmpty in GameObject.FindGameObjectsWithTag("Card"))
        {
            Cards.cardList.Add(new Cards(cardEmpty));
        }

        Shuffle();

        // Print the names of the cards - should be Card 1, Card 2, and Card 3 in a random order
        foreach (Cards cardItem in Cards.cardList)
        {
            print(cardItem.card.name.ToString());
        }

        ViewVector = new Vector3(10f, 10f, -15f);

    }

    void Shuffle()
    {
        for (int i = 0; i < Cards.cardList.Count; i++)
        {
            int rnd = Random.Range(0, Cards.cardList.Count);
            tempGO = Cards.cardList[rnd];
            Cards.cardList[rnd] = Cards.cardList[i];
            Cards.cardList[i] = tempGO;
        }
    }


    void Update()
    {
        // Temporary method - will re-write this so card moves automatically when player lands on given tile
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Move the card so it is readable from the camera's position
            obj.Translate(ViewVector, Camera.main.transform);

            // Rotate the card about its z-axis so we can see its face
            obj.Rotate(0, 0, 180);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {

            // Place the card back in the deck
            obj.Translate(-ViewVector, Camera.main.transform);

            // Rotate the card to hide its face
            obj.Rotate(0, 0, 180);


        }
        
    }
}
