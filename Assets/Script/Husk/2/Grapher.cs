using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grapher : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint2D;
    public bool nowJoint;
    Vector2 jointPoint;
    float verticalInput;
    public float minDistance = 0.1f;
    void Start()
    {
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        distanceJoint2D.enabled = false;

        FindObjectOfType<YeouijuReflect>().CollisionEvent += MakeJoint;
        FindObjectOfType<Yeouiju>().DisjointAction += DeleteJoint;
    }

    void Update()
    {
        // if(distanceJoint2D.distance < minDistance)
        //     nowJoint = false;
            
        if(nowJoint)
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
        Debug.Log("zzz");
        nowJoint = true;
        jointPoint = point;
    }

    public void DeleteJoint()
    {
        nowJoint = false;
    }

}
