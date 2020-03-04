using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int nodId;   //Poisition on the route
    public Text numberText;
    public Node connectedNode;

    public void SetNodeID(int _nodID) 
    {
        nodId = _nodID;
        if(numberText != null)
        {
            numberText.text = nodId.ToString();
        }
    
    
    
    
    }
}
