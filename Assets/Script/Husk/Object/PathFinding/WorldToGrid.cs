using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToGrid : MonoBehaviour
{
    public Transform target;

    public LayerMask groundMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    private float nodeDiameter;
    int gridSizeX, gridSizeY;

    Node[,] grid;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;

        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt((gridWorldSize.x / nodeDiameter));
        gridSizeY = Mathf.RoundToInt((gridWorldSize.y / nodeDiameter)); 

        CreateGrid();       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));

        if(grid == null)    return;

        Node targetNode = NodeFromWroldPosition(target.position);
        foreach(Node n in grid)
        {
            Gizmos.color = (n.canWalk) ? Color.blue : Color.red; 
            if(n == targetNode)
                Gizmos.color = Color.yellow;
            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
        }
    }

    private Node NodeFromWroldPosition(Vector2 WorldPos)
    {
        WorldPos -= (Vector2)transform.position;
		float percentX = (WorldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (WorldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
		
        percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        

		return grid[x,y];
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector2 bottomLeft = (Vector2)transform.position - (Vector2.right * gridWorldSize.x / 2) - (Vector2.up * gridWorldSize.y / 2);

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool canPass = (Physics2D.OverlapCircle(worldPoint, nodeRadius, groundMask) == null);
                grid[x, y] = new Node(canPass, worldPoint, x, y);
            }
        }
    }
}
