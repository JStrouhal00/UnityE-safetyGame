using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Route route;
    public List<Node> nodeList = new List<Node>();

    int routePosition;

    int playerId;
    float speed = 8f;

    int stepsToMove;
    int doneSteps;

    bool isMoving;

    void Start()
    {
        foreach(Transform c in route.nodeList)
        {
            Node n = c.GetComponentInChildren<Node>();
            if (n != null)
            {
                nodeList.Add(n);
            }
        }
        
    }

    
    void Update()
    {
       //Debug 
       if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {   //Roll a Dice
            stepsToMove = Random.Range(1, 7);
            print("Dice rolled " + stepsToMove);

            if(doneSteps + stepsToMove < route.nodeList.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                print("Number is too High");
            }
        }
    }

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;
        while(stepsToMove > 0)
        {
            routePosition++;
            Vector3 nextPos = route.nodeList[routePosition].transform.position;

            while(MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            stepsToMove--;
            doneSteps++;
        }





        isMoving = false;
    }
    bool MoveToNextNode(Vector3 nextPos)
    {
        return nextPos != (transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime));
    }
}
