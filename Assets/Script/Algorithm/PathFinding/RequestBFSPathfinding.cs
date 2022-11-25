using System;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Algorithm.PathFinding
{
    public class RequestBFSPathfinding : MonoBehaviour
    {
        public static RequestBFSPathfinding _instance;
        private BFSPathFinding _bfsPathFinding;
        private WorldToGrid _grid;
        private Queue<PathInfo> _pathToProcess = new Queue<PathInfo>();
        private PathInfo _curPath;
        private bool _isProcessing = false;
        public WorldToGrid _CurGrid { set { _grid = value; } }


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            _bfsPathFinding = GetComponent<BFSPathFinding>();
        }

        public static void RequestPath(Vector2 startPos, Vector2 endPos, Action<Vector2[], bool> callback)
        {
            PathInfo newPath = new PathInfo(startPos, endPos, callback);

            _instance._pathToProcess.Enqueue(newPath);
            _instance.ProcessPathInfo();
        }

        private void ProcessPathInfo()
        {
            if (_isProcessing || _pathToProcess.Count <= 0) return;

            _isProcessing = true;
            _curPath = _pathToProcess.Dequeue();
            _bfsPathFinding.TriggerPathFinding(_grid, _curPath.startPos, _curPath.endPos);
        }

        public void FinishPathFinding(Vector2[] path, bool success)
        {
            _curPath.callback(path, success);
            _isProcessing = false;
            ProcessPathInfo();
        }

        struct PathInfo
        {
            public Vector2 startPos;
            public Vector2 endPos;
            public Action<Vector2[], bool> callback;

            public PathInfo(Vector2 inputStartPos, Vector2 inputEndPos, Action<Vector2[], bool> inputCallback)
            {
                startPos = inputStartPos;
                endPos = inputEndPos;
                callback = inputCallback;
            }
        }
    }
}