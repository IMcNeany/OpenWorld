using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public int gCost;
    public int hCost;
    public int gridX, gridY;
    public Node parent;
    public bool walkable = true;
    public int calculateFCost() { return gCost + hCost; }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name + "coller name");
        if (other.tag == "House" || other.tag == "Rock")
            {
            walkable = false;
            }
        else
        {
            walkable = true;
        }
    }
}


