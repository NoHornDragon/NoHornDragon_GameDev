using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Grapher : MonoBehaviour
{
    public event Action<Vector2> GrapEvent;

    public Camera mainCam;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;
    bool is_joint_now;
    Vector2 joint_point;
    float vertical_input;
    void Start()
    {
        distanceJoint2D.enabled = false;

        FindObjectOfType<YeouijuReflect>().CollisionEvent += MakeJoint;
        FindObjectOfType<Yeouiju>().DisjointAction += DeleteJoint;
    }

    void Update()
    {
        if(is_joint_now)
        {
            vertical_input = Input.GetAxis("Vertical") * -0.2f;

            lineRenderer.SetPosition(0, joint_point);
            lineRenderer.SetPosition(1, transform.position);

            distanceJoint2D.connectedAnchor = joint_point;
            distanceJoint2D.distance += vertical_input;

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
        is_joint_now = true;
        joint_point = point;
    }

    public void DeleteJoint()
    {
        is_joint_now = false;
    }

}
