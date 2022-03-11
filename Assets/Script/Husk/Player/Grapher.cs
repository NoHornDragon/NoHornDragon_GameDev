using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grapher : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint2D;
    [Header("여의주 상태 변수")]
    [Tooltip("origin 모습이 아니면 여의주를 발사하지못하게")] 
    [SerializeField] private bool canUseGrapher; 
    [Tooltip("현재 플레이어가 연결이 되었는지")]    
    [SerializeField] private bool nowJoint;
    [SerializeField] private Vector2 jointPoint;
    private float verticalInput;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        distanceJoint2D.enabled = false;
        canUseGrapher = true;

        FindObjectOfType<YeouijuReflect>().CollisionEvent += MakeJoint;
        FindObjectOfType<Yeouiju>().DisjointAction += DeleteJoint;
        FindObjectOfType<PlayerCollision>().ControlEvent += SetGrapher;
    }

    void Update()
    {

        if(!nowJoint)
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
            return;
        }
        
        if(!canUseGrapher) return;
        
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

    public void SetGrapher(bool input)
    {
        canUseGrapher = input;

        if(input == false)
        {
            nowJoint = false;
        }
    }

}
