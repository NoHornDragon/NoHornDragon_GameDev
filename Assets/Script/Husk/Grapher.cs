using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher : MonoBehaviour
{
    public Camera mainCam;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;
    void Start()
    {
        distanceJoint2D.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePoint = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, mousePoint);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint2D.connectedAnchor = mousePoint;
            distanceJoint2D.enabled = true;
            lineRenderer.enabled = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }

        if(distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
