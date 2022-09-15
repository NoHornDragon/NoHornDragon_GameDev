using System;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Algorithm.PathFinding
{
    public class RequestBFSPathfinding : MonoBehaviour
    {
        public static RequestBFSPathfinding instance;
        private BFSPathFinding bfsPathFinding;
        private WorldToGrid grid;
        private Queue<PathInfo> pathToProcess = new Queue<PathInfo>();
        private PathInfo curPath;
        private bool isProcessing = false;
        public WorldToGrid CurGrid { set { grid = value; } }


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            bfsPathFinding = GetComponent<BFSPathFinding>();
        }

        public static void RequestPath(Vector2 startPos, Vector2 endPos, Action<Vector2[], bool> callback)
        {
            PathInfo newPath = new PathInfo(startPos, endPos, callback);

            instance.pathToProcess.Enqueue(newPath);
            instance.ProcessPathInfo();
        }

        private void ProcessPathInfo()
        {
            if (isProcessing || pathToProcess.Count <= 0) return;

            isProcessing = true;
            curPath = pathToProcess.Dequeue();
            bfsPathFinding.TriggerPathFinding(grid, curPath.startPos, curPath.endPos);
        }

        public void FinishPathFinding(Vector2[] path, bool success)
        {
            curPath.callback(path, success);
            isProcessing = false;
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