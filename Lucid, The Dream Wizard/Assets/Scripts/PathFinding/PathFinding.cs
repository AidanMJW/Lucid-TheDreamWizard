using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    GameObject player;

    public PathFindingGrid grid;
    public float targetDistance = 0.5f;
    Node targetNode;

    List<Node> path = new List<Node>();
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        targetNode = grid.worldPointToNode(new Vector2(player.transform.position.x + 0.05f, player.transform.position.y + 0.05f));
        findPath();
    }


    void findPath()
    {
        path.Clear();

        Node currentNode = grid.worldPointToNode(transform.position);
        path.Add(currentNode);

        while(path[path.Count -1] != targetNode)
        {
            
        }

    }
    
    Node getNextNode(Node currentNode)
    {

        return currentNode;
    }

}
