using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grapher : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint2D;
    [Header("여의주 상태 변수")]
    [SerializeField] private bool nowJoint;
    [SerializeField] private Vector2 jointPoint;
    private float verticalInput;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        distanceJoint2D.enabled = false;

        FindObjectOfType<YeouijuReflect>().CollisionEvent += MakeJoint;
        FindObjectOfType<Yeouiju>().DisjointAction += DeleteJoint;
    }

    void Update()
    {
        if(!nowJoint)
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
            return;
        }
        
        verticalInput = Input.GetAxis("Vertical") * -0.2f;

        lineRenderer.SetPosition(0, jointPoint);
        lineRenderer.SetPosition(1, transform.position);

        distanceJoint2D.connectedAnchor = jointPoint;
        distanceJoint2D.distance += verticalInput;

        distanceJoint2D.enabled = true;
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(1, transform.position);
    }

    public void MakeJoint(Vector2 point)
    {
        jointPoint = point;
        nowJoint = true;
    }

    public void DeleteJoint()
    {
        nowJoint = false;
    }

}
