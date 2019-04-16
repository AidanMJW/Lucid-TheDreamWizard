using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum nodeType { air, platform, ground, onGround, nearGround }

public class Node 
{

    int gridX;
    int gridY;

    public nodeType type;

    int floorDistance;

    float gCost;
    float hCost;

    float fCost;

    public Vector2 position;

    public Node(int x, int y, bool ground, bool onGround, bool nearGround, bool platform, Vector2 pos)
    {
        gridX = x;
        gridY = y;

        type = nodeType.air;
        if (nearGround)
            type = nodeType.nearGround;
        if (onGround)
            type = nodeType.onGround;
        if (ground)
            type = nodeType.ground;
        if (platform)
            type = nodeType.platform;

        position = pos;
    }

}
