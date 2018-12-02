using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {
    int width = 100;
    int height = 100;
    List<Node> nodeArray;

	// Use this for initialization
	void Start () {
        string name = gameObject.transform.name;
        StartCoroutine("Grid");
        nodeArray = new List<Node>();
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
        int x = 1;
        int z = 1;
        for (int i = 0; i < width; i++)
        {
            
            for (int j = 0; j < height; j++)
            {
                
                GameObject instance = (GameObject)Instantiate(Resources.Load("Node"), new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y, gameObject.transform.position.z + z), new Quaternion(0, 0, 0, 1));
                instance.transform.parent = GameObject.Find(name).transform;
               // nodeArray.Add(instance.GetComponent<Node>());
                z += 1;
            }
            z = 1;
            x += 1;

        }
        yield break;
    }
 
}
