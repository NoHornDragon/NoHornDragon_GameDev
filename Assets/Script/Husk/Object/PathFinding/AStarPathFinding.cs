using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AStarPathFinding : MonoBehaviour
{
    private WorldToGrid grid;

    RequestAStarPath requestAStarPath;

    private void Start()
    {
        grid = GetComponent<WorldToGrid>();

        requestAStarPath = GetComponent<RequestAStarPath>();
    }



    public void TriggerPathFinding(Vector2 startPos, Vector2 endPos)
    {
        StartCoroutine(PathFinding(startPos, endPos));
    }

    IEnumerator PathFinding(Vector2 startPos, Vector2 targetPos)
    {
        Vector2[] wayPoint = new Vector2[0];
        bool success = false;

        Node startNode = grid.NodeFromWroldPosition(startPos);
        Node targetNode = grid.NodeFromWroldPosition(targetPos);

        if(startNode.canWalk && targetNode.canWalk)
        {
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
                    // TracePath(startNode, targetNode); 
                    success = true;
                    break;
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
        
        yield return null;

        if(success)
        {
            wayPoint = TracePath(startNode, targetNode);
        }
        requestAStarPath.FinishPathFinding(wayPoint, success);
    }


    Vector2[] TracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node curNode = end;

        while(curNode != start)
        {
            path.Add(curNode);
            curNode = curNode.parent;
        }
        Vector2[] wayPoints = ExplicitPath(path);
        Array.Reverse(wayPoints);

        return wayPoints;
    }

    Vector2[] ExplicitPath(List<Node> path)
    {
        List<Vector2> way = new List<Vector2>();
        Vector2 prevDir = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 newDir = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
            if(prevDir != newDir)
            {
                way.Add(path[i].worldPosition);
                prevDir = newDir;
            }
        }
        return way.ToArray();
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
