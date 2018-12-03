using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    CreateGrid section;
    public List<Node> path;
    private int pathIndex = 0;
    public float speed = 1.0f;
    private bool waitingForPath;
    // Use this for initialization
    void Start()
    {
        string sectionName = gameObject.transform.parent.name;
        GameObject sectionObject = GameObject.Find(sectionName);
        section = sectionObject.GetComponent<CreateGrid>();
        waitingForPath = false;
    }

    //// Update is called once per frame
    void Update()
    {
        if (path != null && path.Count > 0)
        {
            Vector3 target = path[pathIndex].transform.position;
            //target.y = groundOffset;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.LookAt(target);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            if (roundVec(transform.position, 0.2f) == roundVec(target, 0.2f))
            {
                pathIndex++;

                if (pathIndex >= path.Count)
                {
                    path = null;
                    pathIndex = 0;
                }
            }
        }
        else if (!waitingForPath)
        {
            findNewRandomPath();
            waitingForPath = true;

        }
    }

    private Vector3 roundVec(Vector3 vector, float roundTo)
    {
        return new Vector3(
             Mathf.Round(vector.x / roundTo) * roundTo,
             Mathf.Round(vector.y / roundTo) * roundTo,
             Mathf.Round(vector.z / roundTo) * roundTo);
    }


    private void findNewRandomPath()
    {
        Node randomNode = section.getRandomWalkableNode();
        path = section.CreatePath(transform.position, randomNode.transform.position);
        waitingForPath = false;
    }

}

