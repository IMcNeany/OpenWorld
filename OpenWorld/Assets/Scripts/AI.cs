using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    CreateGrid section;
    public List<Node> path;
    private int pathIndex = 0;
    public float speed = 2.0f;
    private bool waitingForPath;
    List<Node> nodeArray = new List<Node>();
    public List<Node> openSet;
    public List<Node> closedSet;
    public List<Node> neighbours;
    public Node start;
    public Node selected;
    float nodeSize = 10.0f;
    int width;
    int height;

    public Node node;
    // Use this for initialization
    void Start()
    {
        string sectionName = gameObject.transform.parent.name;
        GameObject sectionObject = GameObject.Find(sectionName);
        section = sectionObject.GetComponent<CreateGrid>();
        waitingForPath = false;

        nodeArray = section.GetNodes();
        width = section.GetWidth();
        height = section.GetHeight();
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
        else if (!waitingForPath || path == null )
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
        Node randomNode = getRandomWalkableNode();
        path = CreatePath(transform.position, randomNode.transform.position);
        
        waitingForPath = false;
    }

    public List<Node> CreatePath(Vector3 startPosition, Vector3 endPosition)
    {
       
        start = getNodeFromPosition(startPosition);
        Node end = getNodeFromPosition(endPosition);

        if (start == null || end == null)
        {
            Debug.Log("Could not find path from: " + startPosition + " to " + endPosition);
            return null;
        }

        openSet = new List<Node>();
        closedSet = new List<Node>();
        openSet.Clear();
        closedSet.Clear();
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Debug.Log(openSet.Count);
            Node currentNode = openSet[0];

            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].calculateFCost() < currentNode.calculateFCost() //If it has a lower fCost
                || openSet[i].calculateFCost() == currentNode.calculateFCost() // Or if the fCost is the same but the hCost is lower
                && openSet[i].hCost < currentNode.hCost)
                {

                    currentNode = openSet[i];

                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == end)
            {
               // openSet.Add(end);
                return CheckPath(start, end);  
                //End reached trace path back
                
            }

            foreach (Node neighbour in getNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                //Calculate the new lowest costs for the neighbour nodes
                int costToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                if (costToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = costToNeighbour;
                    neighbour.hCost = getDistance(neighbour, end);
                    neighbour.parent = currentNode;    //Set parent node

                    openSet.Add(neighbour);
                }
            }
        }
        Debug.Log("returned null");
        return null;

    }
    public Node getRandomWalkableNode()
    {
        selected = null;

        do
        {
            int index = Random.Range(0, nodeArray.Count - 1);
            selected = nodeArray[index];
        } while (!selected.walkable);

        return selected;
    }

    private Node getNodeFromPosition(Vector3 position)
    {
        int gridX = Mathf.RoundToInt(position.z / nodeSize);
        int gridY = Mathf.RoundToInt(position.x / nodeSize);

        int index = (int)gridX + (gridY * width);
        Node node = null;

        if (index < nodeArray.Count - 1)
        {
            node = nodeArray[index];
        }

        return node;
    }

    private List<Node> CheckPath(Node start, Node end)
    {
        List<Node> path = new List<Node>();

        Node currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private List<Node> getNeighbours(Node node)
    {
        Debug.Log("Neighbours");
        neighbours = new List<Node>();

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if ((Mathf.Abs(x) == Mathf.Abs(y)))
                {
                    continue;
                }

                Debug.Log(node.gridX + " grid x " + node.gridY + " grid y ");
                Debug.Log(x + " x " + y + " y ");
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkY >= 0 && checkY < width && checkX >= 0 && checkX < height)
                {
                    Debug.Log(checkX + checkY * width);
                    neighbours.Add(nodeArray[checkX + checkY * width]);
                }
            }
        }
        return neighbours;
    }

    private int getDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridY - b.gridY);
        int distY = Mathf.Abs(a.gridX - b.gridX);

        if (distY > distX)
        {
            Debug.Log(distX + distY + "distance");
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distY + 10 * (distX - distY);
        }
    }
}

