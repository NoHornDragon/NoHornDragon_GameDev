using System.Collections;
using UnityEngine;

public class Node
{
    public bool canWalk;

    public Vector2 worldPosition;
    public int gridX;
    public int gridY;

    public bool visit = false;

    public Vector2 prevNode;

    public int prevX, prevY;


    public Node(bool inputCanWalk, Vector2 inputWorldPosition, int inputGridX, int inputGridY)
    {
        canWalk = inputCanWalk;
        worldPosition = inputWorldPosition; 
        gridX = inputGridX;
        gridY = inputGridY;
    }

    public void SetPrevNode(Vector2 node)
    {
        prevNode = node;
    }
}
