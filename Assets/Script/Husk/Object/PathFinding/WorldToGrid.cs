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
    private int gridSizeX, gridSizeY;

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }
    
    private List<Vector2> foundPath;

    private int[] moveX = { 0, 0, 1, -1, 1, 1, -1, -1 };
    private int[] moveY = { 1, -1, 0, 0, 1, -1, 1, -1 };
    private bool existPath;

    public Vector2 test;

    private Node[,] grid;

    // from Astar
    public List<Node> path;

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
        Node thisNode = NodeFromWroldPosition(transform.position);
        foreach(Node n in grid)
        {
            Gizmos.color = (n.canWalk) ? Color.blue : Color.red; 
            if(n == targetNode)
                Gizmos.color = Color.yellow;
            if(n == thisNode)
                Gizmos.color = Color.gray;

            // Draw Astar path
            if(path != null && path.Contains(n))
                Gizmos.color = Color.black;

            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
        }
    }

    public Node NodeFromWroldPosition(Vector2 WorldPos)
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

    [ContextMenu("PathFinding")]
    private void FindPath()
    {
        Debug.Log($"Pathfinding");
        Node targetNode = NodeFromWroldPosition(target.position);
        Node FromNode = NodeFromWroldPosition(transform.position);
        Vector2 targetPos = new Vector2(targetNode.gridX, targetNode.gridY);

        Queue<Node> q = new Queue<Node>();
        bool[,] visit = new bool[gridSizeX, gridSizeY];
        q.Enqueue(FromNode);

        Node curNode, nextNode;
        while(q.Count > 0)
        {
            curNode = q.Dequeue();

            if(curNode == targetNode)
            {
                Debug.Log($"founded");
                existPath = true;
                return;
            }
            
            for(int i = 0; i < 8; i++)
            {
                int nextX = curNode.gridX + moveX[i];
                int nextY = curNode.gridY + moveY[i];
                if(NotNPCRange(nextX, nextY))   continue;
                
                nextNode = grid[nextX, nextY];
                if(nextNode.visit)      continue;
                if(!nextNode.canWalk)   continue;

                nextNode.visit = true;
                nextNode.prevX = curNode.gridX;
                nextNode.prevY = curNode.gridY;
                q.Enqueue(nextNode);
            }
        }
        Debug.Log($"not founded");
        existPath = false;


        // q.Enqueue(transform.position);
        // NodeFromWroldPosition(transform.position).visit = true;

        // Vector2 curPos, NextPos;

        // while(q.Count > 0)
        // {
        //     curPos = q.Dequeue();
            
        //     if(NodeFromWroldPosition(curPos) == targetNode) 
        //         break;

        //     for(int i = 0; i < 4; i++)
        //     {
        //         NextPos.x = curPos.x + moveX[i];
        //         NextPos.y = curPos.y + moveY[i];

        //         if(NotNPCRange(NextPos))    continue;

        //         Node nextNode = NodeFromWroldPosition(NextPos);
        //         if(nextNode.visit)  continue;

        //         nextNode.visit = true;
        //         nextNode.SetPrevNode(curPos);
        //         q.Enqueue(NextPos);

        //     }
        // }
    }

    [ContextMenu("print path")]
    private void PrintPath()
    {
        if(!existPath)
        {
            Debug.Log($"no path");
            return;
        }

        Node curNode = NodeFromWroldPosition(target.position);
        Node originNode = NodeFromWroldPosition(transform.position);
        while(true)
        {
            Debug.Log($"cur : {curNode.gridX}, {curNode.gridY}");
            if(curNode == originNode)   break;

            curNode = grid[curNode.prevX, curNode.prevY];
        }
    }

    bool NotNPCRange(int x, int y)
    {
        return (x < 0 || y < 0 || x >= gridSizeX || y >= gridSizeY);
    }
}
