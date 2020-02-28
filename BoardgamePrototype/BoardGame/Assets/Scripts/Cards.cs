using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards
{

    public GameObject card;

    static public List<Cards> cardList = new List<Cards>();

    // Default constructor
    public Cards(GameObject addCard)
    {
        card = addCard;
    }

    

}
