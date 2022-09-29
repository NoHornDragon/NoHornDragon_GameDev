using UnityEngine;

namespace NHD.Algorithm.PathFinding
{
    public class Node : IHeapItem<Node>
    {
        public bool canWalk;

        public Vector2 worldPosition;
        public int gridX;
        public int gridY;

        public bool visit = false;

        public int prevX, prevY;

        // for AStar Algorithm
        public int gCost;
        public int hCost;
        public int fCost { get { return gCost + hCost; } }
        public Node parent;

        // heap
        int heapIndex;


        public Node(bool inputCanWalk, Vector2 inputWorldPosition, int inputGridX, int inputGridY)
        {
            canWalk = inputCanWalk;
            worldPosition = inputWorldPosition;
            gridX = inputGridX;
            gridY = inputGridY;
        }

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }
            set
            {
                heapIndex = value;
            }
        }

        public int CompareTo(Node node)
        {
            int compare = fCost.CompareTo(node.fCost);
            if (compare == 0)
                compare = hCost.CompareTo(node.hCost);

            return -compare;
        }
    }
}