using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Algorithm.PathFinding
{
    public class BFSPathFinding : MonoBehaviour
    {
        [Tooltip("대각선 이동을 할 수 있는지 여부")]
        [SerializeField] private bool _canDiagonalMove;
        private int[] _moveX = { 0, 0, 1, -1, 1, 1, -1, -1 };
        private int[] _moveY = { 1, -1, 0, 0, 1, -1, 1, -1 };

        public void TriggerPathFinding(WorldToGrid grid, Vector2 startPos, Vector2 endPos)
        {
            StartCoroutine(PathFinding(grid, startPos, endPos));
        }

        IEnumerator PathFinding(WorldToGrid grid, Vector2 startPos, Vector2 endPos)
        {
            Vector2[] wayPoint = new Vector2[0];
            bool success = false;

            Node startNode = grid.NodeFromWroldPosition(startPos);
            Node targetNode = grid.NodeFromWroldPosition(endPos);

            if (startNode._canWalk && targetNode._canWalk)
            {
                Queue<Node> q = new Queue<Node>();
                bool[,] visit = new bool[grid.GridSizeX, grid.GridSizeY];
                q.Enqueue(startNode);
                Node curNode, nextNode;
                while (q.Count > 0)
                {
                    curNode = q.Dequeue();

                    if (curNode == targetNode)
                    {
                        success = true;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        int nextX = curNode._gridX + _moveX[i];
                        int nextY = curNode._gridY + _moveY[i];

                        if (nextX < 0 || nextY < 0 || nextX >= grid.GridSizeX || nextY >= grid.GridSizeY) continue;
                        if (visit[nextX, nextY]) continue;
                        nextNode = grid.grid[nextX, nextY];

                        if (!nextNode._canWalk) continue;

                        visit[nextX, nextY] = true;
                        nextNode._parent = curNode;
                        q.Enqueue(nextNode);
                    }

                    if (!_canDiagonalMove) continue;
                    for (int i = 4; i < 8; i++)
                    {
                        int nextX = curNode._gridX + _moveX[i];
                        int nextY = curNode._gridY + _moveY[i];

                        if (nextX < 0 || nextY < 0 || nextX >= grid.GridSizeX || nextY >= grid.GridSizeY) continue;
                        if (visit[nextX, nextY]) continue;
                        nextNode = grid.grid[nextX, nextY];

                        if (!nextNode._canWalk) continue;

                        visit[nextX, nextY] = true;
                        nextNode._parent = curNode;
                        q.Enqueue(nextNode);
                    }

                }

                yield return null;
                if (success)
                {
                    wayPoint = TracePath(startNode, targetNode);
                }
                RequestBFSPathfinding._instance.FinishPathFinding(wayPoint, success);
            }
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
    }
}