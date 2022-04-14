using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerGrapher : MonoBehaviour
{
    public event Action deleteJointEvent;
    [SerializeField] private float lineModifySpeed;
    private LineRenderer lineRenderer;
    private DistanceJoint2D joint;
    private bool nowJoint;
    [SerializeField] private float minDistance;

    void Start()
    {
        lineModifySpeed *= -1;

        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();

        joint.anchor = Vector3.zero;
        SetLine(false);

        FindObjectOfType<YeouijuLaunch>().disJointEvent += DeleteJoint;
        FindObjectOfType<YeouijuReflection>().collisionEvent += MakeJoint;
    }

    void Update()
    {
        if(!nowJoint)
        {
            SetLine(false);
            return;
        }

        

        joint.distance += Input.GetAxis("Vertical") * lineModifySpeed;
        if(joint.distance < minDistance)
            deleteJointEvent?.Invoke();
        lineRenderer.SetPosition(1, this.transform.position);
    }

    public bool NowJoint()
    {
        return nowJoint;
    }

    public void MakeJoint(Vector2 target)
    {
        nowJoint = true;
        // set joint position
        joint.connectedAnchor = target;
        
        // set line renderer
        // this should be in update
        lineRenderer.SetPosition(0, target);
        lineRenderer.SetPosition(1, this.transform.position);

        SetLine(true);
    }

    public void DeleteJoint()
    {
        nowJoint = false;
        SetLine(false);
    }

    void SetLine(bool active)
    {
        lineRenderer.enabled = active;
        joint.enabled = active;
    }
}
