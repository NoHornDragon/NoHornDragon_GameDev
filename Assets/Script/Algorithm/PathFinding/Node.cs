using UnityEngine;

namespace NHD.Algorithm.PathFinding
{
    public class Node : IHeapItem<Node>
    {
        public bool _canWalk;

        public Vector2 _worldPosition;
        public int _gridX;
        public int _gridY;
        public int prevX, prevY;

        // for AStar Algorithm
        public int _gCost;
        public int _hCost;
        public int _fCost { get { return _gCost + _hCost; } }
        public Node _parent;

        // heap
        int _heapIndex;


        public Node(bool inputCanWalk, Vector2 inputWorldPosition, int inputGridX, int inputGridY)
        {
            _canWalk = inputCanWalk;
            _worldPosition = inputWorldPosition;
            _gridX = inputGridX;
            _gridY = inputGridY;
        }

        public int HeapIndex
        {
            get
            {
                return _heapIndex;
            }
            set
            {
                _heapIndex = value;
            }
        }

        public int CompareTo(Node node)
        {
            int compare = _fCost.CompareTo(node._fCost);
            if (compare == 0)
                compare = _hCost.CompareTo(node._hCost);

            return -compare;
        }
    }
}