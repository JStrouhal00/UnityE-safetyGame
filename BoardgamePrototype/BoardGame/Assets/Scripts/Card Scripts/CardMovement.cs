using UnityEngine;

public class CardMovement : MonoBehaviour
{
    //public Transform obj;
    float countdown;
    bool isPlayerColliding = false;
    public QuestionManager qm;
    public ChanceManager cm;

    private void Start()
    {
        countdown = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a collision between the player and our node/tile, the "draw card" countdown starts
        if (isPlayerColliding)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0) // This is just to ensure the countdown cannot go below 0
            {
                countdown = 0;
            }
        }
    }

    // Start the countdown when our player enters the collision range
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("QuestionTrigger") || other.gameObject.CompareTag("ChanceTrigger"))
        {
            isPlayerColliding = true;
        }
    }

    // If the player is staying still, chances are they've stopped on that tile, so move the cards
    void OnTriggerStay(Collider other)
    {
        if (isPlayerColliding)
        {
            if (countdown == 0)
            {
                if (other.gameObject.CompareTag("QuestionTrigger"))
                {
                    qm.TriggerNextQuestion();
                    isPlayerColliding = false;  // Reset the boolean so the event only occurs once
                }
                if (other.gameObject.CompareTag("ChanceTrigger"))
                {
                    cm.TriggerNextChance();
                    isPlayerColliding = false;
                }
            }
        }
    }

    // If the player is no longer colliding, they've probably continued moving on and we can reset everything
    void OnTriggerExit(Collider other)
    {
        isPlayerColliding = false;
        countdown = 2.0f;
    }
    
}
