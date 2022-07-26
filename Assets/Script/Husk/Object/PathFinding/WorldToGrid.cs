using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToGrid : MonoBehaviour
{
    public Transform target;

    [Header("For Grid")]
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private Vector2 gridWorldSize;
    [SerializeField]
    private Vector2 gridOffset;
    public Vector2 GridOffset { get { return gridOffset; } }
    public float nodeRadius;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    public int GridSizeX { get { return gridSizeX; } }
    public int GridSizeY { get { return gridSizeY; } }
    public Node[,] grid;
    [SerializeField]
    private bool drawGizmos;
    public Vector2 test;

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }


    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;

        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt((gridWorldSize.x / nodeDiameter));
        gridSizeY = Mathf.RoundToInt((gridWorldSize.y / nodeDiameter)); 

        CreateGrid();       
    }

    private void OnEnable()
    {
        RequestAStarPath.instance.CurGrid = this;
    }

    private void OnDrawGizmos()
    {
        if(!drawGizmos) return;

        Gizmos.DrawWireCube(transform.position + (Vector3)gridOffset, new Vector2(gridWorldSize.x, gridWorldSize.y));

        if(grid == null)    return;

        Node targetNode = NodeFromWroldPosition(target.position);
        Node thisNode = NodeFromWroldPosition(transform.position);
        foreach(Node n in grid)
        {
            Gizmos.color = (n.canWalk) ? Color.blue : Color.red; 
            if(n == targetNode)
                Gizmos.color = Color.yellow;

            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
        }
    }

    public Node NodeFromWroldPosition(Vector2 WorldPos)
    {
        WorldPos -= (Vector2)transform.position + gridOffset;
		float percentX = (WorldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (WorldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
		
        percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid[x,y];
    }

    public List<Node> GetNodeNeighbours(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)    continue;

                int nodeX = node.gridX + x;
                int nodeY = node.gridY + y;

                if(NotNPCRange(nodeX, nodeY))   continue;

                neighbors.Add(grid[nodeX, nodeY]);
            }
        }

        return neighbors;
    }


    [ContextMenu("Create Grid")]
    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector2 bottomLeft = (Vector2)transform.position - (Vector2.right * gridWorldSize.x / 2) - (Vector2.up * gridWorldSize.y / 2) + test;
        bottomLeft += gridOffset;
        
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

    bool NotNPCRange(int x, int y)
    {
        return (x < 0 || y < 0 || x >= gridSizeX || y >= gridSizeY);
    }
}
