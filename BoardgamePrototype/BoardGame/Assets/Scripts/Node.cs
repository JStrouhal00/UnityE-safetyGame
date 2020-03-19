using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int nodeId;   //Poisition on the route
    public Text numberText;
    public Node connectedNode;

    public void SetNodeID(int _nodeID) 
    {
        nodeId = _nodeID;
        if(numberText != null)
        {
            numberText.text = nodeId.ToString();
        }
    
    
    
    
    }
    void OnDrawGizmos()
    {
        if (connectedNode != null)
        {
            Color col = Color.white;

            col = (connectedNode.nodeId > nodeId) ? Color.blue : Color.red;
            Debug.DrawLine(transform.position, connectedNode.transform.position, col);
        }
    }
}

