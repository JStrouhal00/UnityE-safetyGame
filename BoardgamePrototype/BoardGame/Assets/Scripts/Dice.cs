﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;

    bool hasLanded;
    bool thrown;

    Vector3 initPosition;

    public DiceSide[] diceSides;
    public int diceValue;


    void Start()
    {
        initPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void RollDice()
    {
        //RESET
        Reset();

        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if(thrown && hasLanded)
        {
            //RESET
            Reset();
        }
    }

    void Reset()
    {
        transform.position = initPosition;
        rb.isKinematic = false;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
    }

    void Update()
    {
        if(rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            //SIDE VALUE CHECK
            SidevalueCheck();
        }
        else if(rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            //ROLL AGAIN
            RollAgain();
        }
    }

    void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    void SidevalueCheck()
    {
        diceValue = 0;
        foreach(DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                //SEND THE RESULT TO THE GAMEMANAGER
                GameManager.instance.RolledNumber(diceValue);
            }
        }
    }
}
