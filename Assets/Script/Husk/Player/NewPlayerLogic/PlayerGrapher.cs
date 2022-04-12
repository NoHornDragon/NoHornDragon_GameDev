using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapher : MonoBehaviour
{
    [SerializeField] private float lineModifySpeed;
    [SerializeField] private Transform grapherPoint;
    private LineRenderer lineRenderer;
    private DistanceJoint2D joint;
    private float verticalInput;
    private bool nowJoint;

    void Start()
    {
        lineModifySpeed *= -1;

        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();

        joint.anchor = this.transform.position;
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

        
        // joint.anchor = this.transform.localPosition;
        joint.distance += Input.GetAxis("Vertical") * lineModifySpeed;
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
