using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher2 : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;
    bool isJointNow;
    Vector2 jointPoint;
    float verticalInput;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        distanceJoint2D.enabled = false;

        FindObjectOfType<YeouijuMoving>().MakeJoint2D += MakeJoint;
        FindObjectOfType<YeouijuParabola>().DeleteJoint2D += DeleteJoint;
    }

    void Update()
    {
        if(isJointNow)
        {
            verticalInput = Input.GetAxis("Vertical") * -0.2f;

            lineRenderer.SetPosition(0, jointPoint);
            lineRenderer.SetPosition(1, transform.position);

            distanceJoint2D.connectedAnchor = jointPoint;
            distanceJoint2D.distance += verticalInput;

            distanceJoint2D.enabled = true;
            lineRenderer.enabled = true;
        }
        else
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }

        if(distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    public void MakeJoint(Vector2 point)
    {
        isJointNow = true;
        jointPoint = point;
    }

    public void DeleteJoint()
    {
        isJointNow = false;
    }
}
