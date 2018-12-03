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
    private void Start()
    {
      
    walkable = true;
    }

    //private void OnTriggerStay(Collider other)
    //{
     
    //    if (other.gameObject.GetComponent<Camera>())
    //    {
            
    //    }
    //    else if(other.transform.parent.tag == "Terrain")
    //    {
    //        Debug.Log("False");
    //        walkable = true;
    //    }
    //    else if (other.transform.parent.tag == "House" || other.transform.parent.tag == "Rock")
    //        {
    //        Debug.Log("False");
    //        walkable = false;
    //        }
    //    else
    //    {
    //        Debug.Log("elseeee");
    //        walkable = true;
    //    }
    //}
}


