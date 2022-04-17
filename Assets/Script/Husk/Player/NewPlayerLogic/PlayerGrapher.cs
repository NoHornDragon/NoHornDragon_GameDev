using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PlayerGrapher : MonoBehaviour
{
    // this event = YeouijuLaunch.disJointEvent
    public event Action deleteJointEvent;
    [SerializeField] private float lineModifySpeed;
    private LineRenderer lineRenderer;
    private DistanceJoint2D joint;
    [Header("여의주 상태")]
    private bool nowJoint;
    [SerializeField] private float minDistance;
    [SerializeField] private float jointMaxTime;
    private float jointTimer;
    [Header("여의주 HUD")]
    [SerializeField] GameObject coolTimeUI;
    [SerializeField] private Image coolTimeImage;


    void Start()
    {
        lineModifySpeed *= -1;

        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();

        joint.anchor = Vector3.zero;
        SetLine(false);

        FindObjectOfType<YeouijuLaunch>().disJointEvent += DeleteJoint;
        FindObjectOfType<YeouijuLaunch>().disJointEvent += SetHUDInitial;
        FindObjectOfType<YeouijuReflection>().collisionEvent += MakeJoint;
    }

    void Update()
    {
        if(!nowJoint)
        {
            SetLine(false);
            jointTimer = jointMaxTime;
            SetHUDInitial();
            return;
        }

        joint.distance += Input.GetAxis("Vertical") * lineModifySpeed;
        jointTimer -= Time.deltaTime;
        coolTimeImage.fillAmount = jointTimer / jointMaxTime;

        if(joint.distance < minDistance)
            deleteJointEvent?.Invoke();
        if(jointTimer < 0)
        {
            nowJoint = false;
            deleteJointEvent?.Invoke();
        }

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
        lineRenderer.SetPosition(0, target);
        lineRenderer.SetPosition(1, this.transform.position);

        // active cool time ui
        coolTimeUI.SetActive(true);

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

    private void SetHUDInitial()
    {
        coolTimeImage.fillAmount = 1;
        coolTimeUI.SetActive(false);
    }
}
