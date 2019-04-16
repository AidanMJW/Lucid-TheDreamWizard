using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingGrid : MonoBehaviour
{

    public int gridWidth;
    public int gridHeight;

    [Space(10)]
    public float nodeSize;
    public float nodeDistance;

    [Space(10)]
    public int nodeJumpHeight;
    public int nodeJumpWidth;

    [Space(10)]
    public bool dynamicPlatforms;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    LayerMask testLayer;

    public Node[,] theGrid;

    int _gridWidth;
    int _gridHeight;

    GameObject player;

    private void Start()
    {
        testLayer = groundLayer | platformLayer;
        player = GameObject.FindGameObjectWithTag("Player");
        createGrid();
    }

    private void Update()
    {
        if(dynamicPlatforms)
        {
            if(Vector3.Distance(new Vector3(transform.position.x + gridWidth / 2, transform.position.y + gridHeight / 2,0) , player.transform.position) < gridWidth)
            {
                createGrid();
            }
        }        
    }

    void createGrid()
    {
        _gridWidth = (int)(gridWidth / (nodeSize + nodeDistance));
        _gridHeight = (int)(gridHeight / (nodeSize + nodeDistance));

        theGrid = new Node[_gridWidth, _gridHeight];
        float nodeDist = nodeSize + nodeDistance;

        for(int x = 0; x < _gridWidth; x++)
        {
            for(int y = 0; y < _gridHeight; y++)
            {
                //get position
                Vector2 nodePosition = new Vector2(transform.position.x + (nodeDist * x), transform.position.y + (nodeDist * y));

                //check for ground
                bool isGround = Physics2D.OverlapBox(nodePosition, new Vector2(nodeSize, nodeSize), 0, groundLayer);

                //check for platform
                bool isPlatform = Physics2D.OverlapBox(nodePosition, new Vector2(nodeSize, nodeSize), 0, platformLayer);

                //check if on ground
                bool onGround = false;
                if(isGround == false)
                {
                    onGround = Physics2D.OverlapBox(new Vector2(nodePosition.x, nodePosition.y - nodeDist), new Vector2(nodeSize, nodeSize), 0, testLayer);
                }

                //check of near ground
                bool nearGound = false;
                bool test = false;

                if(isGround == false && onGround == false)
                {
                    for(int i = 0; i < nodeJumpHeight; i++)
                    {
                        test = Physics2D.OverlapBox(new Vector2(nodePosition.x, nodePosition.y - (nodeDist * i)), new Vector2(nodeSize, nodeSize), 0, testLayer);
                        if (test == true)
                            nearGound = true;

                        for (int a = 0; a < nodeJumpWidth; a++)
                        {
                            test = Physics2D.OverlapBox(new Vector2(nodePosition.x + (nodeDist * a), nodePosition.y - (nodeDist * i)), new Vector2(nodeSize, nodeSize), 0, testLayer);
                            if (test == true)
                                nearGound = true;
                            test = Physics2D.OverlapBox(new Vector2(nodePosition.x - (nodeDist * a), nodePosition.y - (nodeDist * i)), new Vector2(nodeSize, nodeSize), 0, testLayer);
                            if (test == true)
                                nearGound = true;
                        }
                    }
                }

                theGrid[x, y] = new Node(x, y, isGround, onGround, nearGound, isPlatform, nodePosition);
            }
        }
    }

    
    private void OnDrawGizmos()
    {
        if(theGrid != null)
        {
            foreach (Node n in theGrid)
            {
                if (n.type == nodeType.nearGround)
                {
                    Gizmos.color = Color.yellow;
                }
                else if (n.type == nodeType.onGround)
                {
                    Gizmos.color = Color.green;
                }
                else if (n.type == nodeType.ground)
                {
                    Gizmos.color = Color.red;
                }
                else if (n.type == nodeType.platform)
                {
                    Gizmos.color = Color.magenta;
                }
                else 
                {
                    Gizmos.color = Color.white;
                }

                Gizmos.DrawCube(n.position, new Vector3(nodeSize, nodeSize, 1));
            }
        }

    }
    

}
