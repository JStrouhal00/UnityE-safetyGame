using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public Transform obj;
    private Vector3 ViewVector;
    public float Countdown = 1.0f;
    bool isPlayerColliding = false;

    // Start the countdown when our player enters the collision range
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Stone")
        {
            isPlayerColliding = true;
        }
    }

    // If the player is staying still, chances are they've stopped on that tile, so move the cards
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Stone" && isPlayerColliding == true)
        {
            if (Countdown == 0)
            {
                // Move the card so it is readable from the camera's position
                obj.Translate(ViewVector, Camera.main.transform);

                // Rotate the card about its z-axis so we can see its face
                obj.Rotate(0, 0, 180);
                isPlayerColliding = false;
            }
        }
    }

    // If the player is no longer colliding, they've probably continued moving on and we can reset our countdown
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Stone")
        {
            isPlayerColliding = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If there is a collision between the player and our node/tile, the countdown starts
        if (isPlayerColliding == true)
        {
            Countdown -= Time.deltaTime;
            if (Countdown <= 0) // This is just to ensure the countdown cannot go below 0
            {
                Countdown = 0;
            }
        }
    }
}
