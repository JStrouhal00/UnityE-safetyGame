using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public Transform obj;
    private Vector3 ViewVector;
    float Countdown;
    bool isPlayerColliding = false;

    private void Start()
    {
        ViewVector = new Vector3(9f, 5f, -9f);
    }

    // Start the countdown when our player enters the collision range
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Countdown = 2.0f;
            isPlayerColliding = true;
        }
    }

    // If the player is staying still, chances are they've stopped on that tile, so move the cards
    void OnTriggerStay(Collider other)
    {
        if (isPlayerColliding)
        {
            if (Countdown == 0)
            {
                // Move the card so it is readable from the camera's position
                obj.Translate(ViewVector, Camera.main.transform);

                // Rotate the card about its z-axis so we can see its face
                obj.Rotate(0, 0, 180);

                isPlayerColliding = false;  // Reset the boolean so the event only occurs once
            }
        }
    }

    // If the player is no longer colliding, they've probably continued moving on and we can reset everything
    void OnTriggerExit(Collider other)
    {
        isPlayerColliding = false;
        Countdown = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a collision between the player and our node/tile, the countdown starts
        if (isPlayerColliding)
        {
            Countdown -= Time.deltaTime;
            if (Countdown <= 0) // This is just to ensure the countdown cannot go below 0
            {
                Countdown = 0;
                //isPlayerColliding = false;  // Reset the boolean so the event only occurs once
            }
        }
    }
}
