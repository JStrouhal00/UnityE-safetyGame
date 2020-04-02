using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public Transform obj;
    private Vector3 ViewVector;
    float dCountdown;
    float rCountdown;
    bool isPlayerColliding = false;
    Vector3 startPos;
    //Vector3 newPos;
    bool outOfPosition = false;

    private void Start()
    {
        ViewVector = new Vector3(9f, 5f, -9f); // The vector that will move the cards into/out of a readable position
        startPos = obj.position; // Save the position of the card when the game starts
        dCountdown = 2.0f;
        rCountdown = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a collision between the player and our node/tile, the "draw card" countdown starts
        if (isPlayerColliding)
        {
            dCountdown -= Time.deltaTime;
            if (dCountdown <= 0) // This is just to ensure the countdown cannot go below 0
            {
                dCountdown = 0;
            }
        }

        // If our card has moved from it's original position, the "return card" countdown starts
        if (outOfPosition)
        {
            rCountdown -= Time.deltaTime;
            if (rCountdown <= 0)
            {
                rCountdown = 0;
            }
        }
    }

    // Start the countdown when our player enters the collision range
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            isPlayerColliding = true;
        }
    }

    // If the player is staying still, chances are they've stopped on that tile, so move the cards
    void OnTriggerStay(Collider other)
    {
        if (isPlayerColliding)
        {
            if (dCountdown == 0)
            {
                // Move the card so it is readable from the camera's position
                obj.Translate(ViewVector, Camera.main.transform);

                // Rotate the card about its z-axis so we can see its face
                obj.Rotate(0, 0, 180);

                //newPos = obj.position; // Store the new card position
                outOfPosition = true;
                isPlayerColliding = false;  // Reset the boolean so the event only occurs once
            }
        }

        if (outOfPosition)
        {
            if (rCountdown == 0)
            {
                // Move the card so it is no longer readable from the camera's position
                obj.Translate(-ViewVector, Camera.main.transform);

                // Rotate the card about its z-axis so we can't see its face
                obj.Rotate(0, 0, 180);

                outOfPosition = false; // Again, reset the boolean
            }
        }
    }

    // If the player is no longer colliding, they've probably continued moving on and we can reset everything
    void OnTriggerExit(Collider other)
    {
        isPlayerColliding = false;
        outOfPosition = false;
        dCountdown = 2.0f;
        rCountdown = 5.0f;
    }
    
}
