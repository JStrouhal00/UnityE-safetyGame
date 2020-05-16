using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public Route route;
    public List<Node> nodeList = new List<Node>();

    int routePosition;

    int playerId;
    float speed = 8f;

    int stepsToMove;
    int doneSteps;

    public bool isMoving;

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

    
   

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;
        while(stepsToMove != 0)
        {
           
            routePosition += stepsToMove < 0 ? -1 : 1;
            if (routePosition > route.nodeList.Count-1) {
                routePosition--;
                break;
            }
            Vector3 nextPos = route.nodeList[routePosition].transform.position;

            while(MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            stepsToMove -= stepsToMove < 0 ? -1 : 1;
            doneSteps += stepsToMove < 0 ? -1 : 1;
            
        }

        yield return new WaitForSeconds(0.1f);
        //Snake & Ladder Check
        if (nodeList[routePosition].connectedNode != null)
        {
            int coNodeId = nodeList[routePosition].connectedNode.nodeId;
            Vector3 nextPos = nodeList[routePosition].connectedNode.transform.position;

            while (MoveToNextNode(nextPos)) { yield return null; }
            routePosition = coNodeId;
        }
        //Check for a win


        //Update the gamemanager
        if (!GetComponent<CardMovement>().isPlayerColliding)
            GameManager.instance.state = GameManager.States.SWITCH_PLAYER;



        isMoving = false;
    }
    bool MoveToNextNode(Vector3 nextPos)
    {
        return nextPos != (transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime));
    }

    public void MakeTurn(int diceNumber)
    {
        stepsToMove = diceNumber;
        StartCoroutine(Move());
        /*
        if (doneSteps + stepsToMove <= route.nodeList.Count)
        {
            
        }
        else
        {
            //print("Number is too High");
            //UPDATE THE GAMEMANAGER
            GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
        }
        */
    }
}
