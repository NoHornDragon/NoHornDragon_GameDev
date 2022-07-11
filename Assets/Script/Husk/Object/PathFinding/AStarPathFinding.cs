using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
    private WorldToGrid grid;

    public Transform finder, target;

    private void Start()
    {
        grid = GetComponent<WorldToGrid>();
        finder = this.transform;
    }


    private void Update()
    {
        PathFinding(finder.position, target.position);
    }


    public void PathFinding(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromWroldPosition(startPos);
        Node targetNode = grid.NodeFromWroldPosition(targetPos);

        // List<Node> openSet = new List<Node>();
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            // Node curNode = openSet[0];
            // for(int i = 0; i < openSet.Count; i++)
            // {
            //     if(curNode.fCost > openSet[i].fCost || openSet[i].fCost == curNode.fCost && openSet[i].hCost < curNode.hCost)
            //         curNode = openSet[i];
            // }

            // openSet.Remove(curNode);
            Node curNode = openSet.RemoveFirst();
            closedSet.Add(curNode);

            if(curNode == targetNode)
            {
                TracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbour in grid.GetNodeNeighbours(curNode))
            {
                if(!neighbour.canWalk || closedSet.Contains(neighbour)) continue;

                int newCost = curNode.gCost + GetDistance(curNode, neighbour);

                if(newCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = curNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }


    public void TracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node curNode = end;

        while(curNode != start)
        {
            path.Add(curNode);
            curNode = curNode.parent;
        }
        path.Reverse();
        grid.path = path;
    }

    public int GetDistance(Node A, Node B)
    {
        int distX = Mathf.Abs(A.gridX - B.gridX);
        int distY = Mathf.Abs(A.gridY - B.gridY);

        if(distX > distY)
            return 4 * distY + 10 * distX;
        
        return 4 * distX + 10 * distY;
    }
}
