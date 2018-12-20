using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {
    int width = 10;
    int height = 10;
  
    public List<Node> nodeArray = new List<Node>();

    // Use this for initialization
    void Start () {
        string name = gameObject.transform.name;
        StartCoroutine("Grid");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDrawGizmos()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Debug.DrawLine(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,gameObject.transform.position.z),new Vector3(gameObject.transform.position.x +100, gameObject.transform.position.y, gameObject.transform.position.z));
                j++;
            }
            Debug.DrawLine(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 100));
            i++;
        }
    }

    IEnumerator Grid()
    {
        int x = 5;
        int z = 5;
        for (int i = 0; i < width; i++)
        {
            
            for (int j = 0; j < height; j++)
            {
                
                GameObject instance = (GameObject)Instantiate(Resources.Load("Node"), new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y, gameObject.transform.position.z + z), new Quaternion(0, 0, 0, 1));
                Node nodeInstance = instance.GetComponent<Node>();
                nodeArray.Add(nodeInstance);
                instance.transform.parent = GameObject.Find(name).transform;
                nodeInstance.gridX = i;
                nodeInstance.gridY = j;
               
                z += 10;
            }
            z = 5;
            x += 10;

        }
        yield break;
    }

  public List<Node> GetNodes()
    {
        return nodeArray;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
