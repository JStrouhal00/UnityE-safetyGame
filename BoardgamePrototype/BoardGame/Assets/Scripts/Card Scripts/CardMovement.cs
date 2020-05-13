using UnityEngine;

public class CardMovement : MonoBehaviour
{
    //public Transform obj;
    float dCountdown;
    float rCountdown;
    public bool isPlayerColliding = false;
    bool outOfPosition = false;
    public QuestionManager qm;

    private void Start()
    {
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
        if (other.gameObject.CompareTag("QuestionTrigger"))
        {
            isPlayerColliding = true;
        }

        if (other.gameObject.CompareTag("End")) {
            GameManager.instance.GetActivePlayer().AddScore(40);
            GameManager.instance.WinTrigger();
        }
    }

    // If the player is staying still, chances are they've stopped on that tile, so move the cards
    void OnTriggerStay(Collider other)
    {
        if (isPlayerColliding)
        {
            if (dCountdown == 0)
            {
                /*// Move the card so it is readable from the camera's position
                obj.Translate(ViewVector, Camera.main.transform);

                // Rotate the card about its z-axis so we can see its face
                obj.Rotate(0, 0, 180);

                //newPos = obj.position; // Store the new card position
                outOfPosition = true;*/

                //Debug.Log("Question Card Moved!");
                QuestionMarkAnimator anim = other.GetComponentInChildren<QuestionMarkAnimator>();
                if(anim != null)
                {
                    anim.MoveInFrontOfCamera(() => qm.TriggerNextQuestion(anim));
                }
                else
                {
                    qm.TriggerNextQuestion();
                }
                isPlayerColliding = false;  // Reset the boolean so the event only occurs once
            }
        }

        /*if (outOfPosition)
        {
            if (rCountdown == 0)
            {
                // Move the card so it is no longer readable from the camera's position
                obj.Translate(-ViewVector, Camera.main.transform);

                // Rotate the card about its z-axis so we can't see its face
                obj.Rotate(0, 0, 180);

                outOfPosition = false; // Again, reset the boolean
            }
        }*/
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
