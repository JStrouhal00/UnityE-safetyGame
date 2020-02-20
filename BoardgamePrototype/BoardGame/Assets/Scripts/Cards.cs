using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{

    public Transform obj;
    private Vector3 ViewVector;
    private Vector3 HideVector;

    public List<GameObject> CardList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       /*?foreach (GameObject c in CardList)
        {
            CardList.AddRange(GameObject.FindGameObjectsWithTag("Card"));
        }*/

        ViewVector = new Vector3(10f, 10f, -15f);
        HideVector = new Vector3(-10f, -12f, 15f);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(CardList);

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
