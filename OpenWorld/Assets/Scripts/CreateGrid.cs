using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {
    int width = 10;
    int height = 10;
    int radius = 10;
	// Use this for initialization
	void Start () {
        OnDrawGizmos();
        Grid();
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

    void Grid()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {

                j++;
            }
            i++;
        }
    }

}
