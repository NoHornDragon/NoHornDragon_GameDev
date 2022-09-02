using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RequestAStarPath : MonoBehaviour
{
    Queue<PathInfo> pathToProcess = new Queue<PathInfo>();
    bool isProcessing;
    PathInfo curPath;

    public static RequestAStarPath instance;
    private AStarPathFinding aStarPathFinding;
    [SerializeField]
    private WorldToGrid grid;
    public WorldToGrid CurGrid { set { grid = value; } }



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(this.gameObject);
        }

        aStarPathFinding = GetComponent<AStarPathFinding>();
    }

    public static void RequestPath(Vector2 startPos, Vector2 endPos, Action<Vector2[], bool> callback)
    {
        PathInfo newPath = new PathInfo(startPos, endPos, callback);
        
        instance.pathToProcess.Enqueue(newPath);
        instance.ProcessPathInfo();
    }

    private void ProcessPathInfo()
    {
        if(isProcessing || pathToProcess.Count <= 0)    return;

        isProcessing = true;
        curPath = pathToProcess.Dequeue();
        aStarPathFinding.TriggerPathFinding(grid, curPath.startPos, curPath.endPos);
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
