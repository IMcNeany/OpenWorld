using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Quaternion rotationDirection;
    private float speed = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
	}

    void UpdateMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            // transform.position += new Vector3(0,0, 5 * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * speed;
            rotationDirection = new Quaternion(0, 0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // transform.position += new Vector3(0,0, -5 * Time.deltaTime);
            transform.position -= transform.forward * Time.deltaTime * speed;
            rotationDirection = new Quaternion(0, 0, 180, 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // transform.position += new Vector3(-5 * Time.deltaTime, 0, 0);
            transform.position -= transform.right * Time.deltaTime * speed;
            rotationDirection = new Quaternion(0, 0, 90, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
            transform.position += transform.right * Time.deltaTime * speed;
            rotationDirection = new Quaternion(0, 0, 270, 1);
        }
       
       // transform.rotation = rotationDirection;
    }
}
