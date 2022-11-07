using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Algorithm.PathFinding
{
    public class AStarPathFinding : MonoBehaviour
    {

        public void TriggerPathFinding(WorldToGrid grid, Vector2 startPos, Vector2 endPos)
        {
            StartCoroutine(PathFinding(grid, startPos, endPos));
        }

        IEnumerator PathFinding(WorldToGrid grid, Vector2 startPos, Vector2 targetPos)
        {
            Vector2[] wayPoint = new Vector2[0];
            bool success = false;

            Node startNode = grid.NodeFromWroldPosition(startPos);
            Node targetNode = grid.NodeFromWroldPosition(targetPos);

            if (startNode._canWalk && targetNode._canWalk)
            {
                Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
                HashSet<Node> closedSet = new HashSet<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node curNode = openSet.RemoveFirst();
                    closedSet.Add(curNode);

                    if (curNode == targetNode)
                    {
                        success = true;
                        break;
                    }

                    foreach (Node neighbour in grid.GetNodeNeighbours(curNode))
                    {
                        if (!neighbour._canWalk || closedSet.Contains(neighbour)) continue;

                        int newCost = curNode._gCost + GetDistance(curNode, neighbour);

                        if (newCost < neighbour._gCost || !openSet.Contains(neighbour))
                        {
                            neighbour._gCost = newCost;
                            neighbour._hCost = GetDistance(neighbour, targetNode);
                            neighbour._parent = curNode;

                            if (!openSet.Contains(neighbour))
                            {
                                openSet.Add(neighbour);
                            }
                        }
                    }
                }
            }

            yield return null;

            if (success)
            {
                wayPoint = TracePath(startNode, targetNode);
            }
            RequestAStarPath._instance.FinishPathFinding(wayPoint, success);
        }


        private Vector2[] TracePath(Node start, Node end)
        {
            List<Node> path = new List<Node>();
            Node curNode = end;

            while (curNode != start)
            {
                path.Add(curNode);
                curNode = curNode._parent;
            }
            Vector2[] wayPoints = MakePathSimple(path);
            Array.Reverse(wayPoints);

            return wayPoints;
        }

        private Vector2[] MakePathSimple(List<Node> path)
        {
            List<Vector2> way = new List<Vector2>();
            Vector2 prevDir = Vector2.zero;
            way.Add(path[0]._worldPosition);

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 newDir = new Vector2(path[i - 1]._gridX - path[i]._gridX, path[i - 1]._gridY - path[i]._gridY);
                if (prevDir != newDir)
                {
                    way.Add(path[i]._worldPosition);
                    prevDir = newDir;
                }
            }
            return way.ToArray();
        }

        public int GetDistance(Node A, Node B)
        {
            int distX = Mathf.Abs(A._gridX - B._gridX);
            int distY = Mathf.Abs(A._gridY - B._gridY);

            if (distX > distY)
                return 4 * distY + 10 * distX;

            return 4 * distX + 10 * distY;
        }
    }
}
