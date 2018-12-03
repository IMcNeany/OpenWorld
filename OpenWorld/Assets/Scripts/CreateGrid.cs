using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {
    int width = 10;
    int height = 10;
    float nodeSize = 10.0f;
    public List<Node> nodeArray = new List<Node>();
    public List<Node> openSet;
    public List<Node> closedSet;
    public List<Node> neighbours;
    public Node start;
    public Node selected;

    public Node node;
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
               
                z += 10;
            }
            z = 5;
            x += 10;

        }
        yield break;
    }

    public List<Node> CreatePath(Vector3 startPosition,Vector3 endPosition)
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

                return CheckPath(start, end);    //End reached trace path back
            }

            foreach (Node neighbour in getNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

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
        Debug.Log( "returned null");
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
            Debug.Log(distX+  distY + "distance");
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distY + 10 * (distX - distY);
        }
    }
}
