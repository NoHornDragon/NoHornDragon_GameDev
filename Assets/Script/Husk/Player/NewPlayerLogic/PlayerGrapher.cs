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
    [SerializeField] private bool easyMode;
    [SerializeField] private float minDistance;
    [SerializeField] private float jointMaxTime;
    [SerializeField] private float jointTimer;
    [SerializeField] private float maxModifiable;
    private float curModifiable;
    [Header("여의주 HUD")]
    [SerializeField] private GameObject coolTimeUI;
    [SerializeField] private Image coolTimeImage;


    void Start()
    {
        SetHUDInitial();
        lineModifySpeed *= -1;
        

        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();

        joint.anchor = Vector3.zero;
        SetLine(false);

        FindObjectOfType<YeouijuLaunch>().disJointEvent += DeleteJoint;
        FindObjectOfType<YeouijuReflection>().collisionEvent += MakeJoint;
        
        easyMode = SaveData.instance.userData.UseEasyMode;
        if(easyMode)
        {
            coolTimeImage = null;
            coolTimeUI = null;
            return;
        }
        // time limit is on only hard mode
        FindObjectOfType<YeouijuLaunch>().disJointEvent += SetHUDInitial;
        FindObjectOfType<YeouijuReflection>().collisionEvent += ActiveUI;
    }

    void Update()
    {
        if(!nowJoint)
        {
            SetLine(false);
            jointTimer = jointMaxTime;
            if(!easyMode)
                SetHUDInitial();
            return;
        }

        float modify = Input.GetAxis("Vertical") * lineModifySpeed;
        joint.distance += modify;

        curModifiable -= Mathf.Abs(modify);


        if(joint.distance < minDistance || curModifiable < 0)
            deleteJointEvent?.Invoke();
        
        lineRenderer.SetPosition(1, this.transform.position);

        if(easyMode)    return;

        // if game is hard mode, Timer is on
        jointTimer -= Time.deltaTime;
        coolTimeImage.fillAmount = jointTimer / jointMaxTime;

        if(jointTimer < 0)
        {
            nowJoint = false;
            deleteJointEvent?.Invoke();
        }

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

        SetLine(true);
    }

    public void ActiveUI(Vector2 dummy)
    {
        coolTimeUI.SetActive(true);
    }

    public void DeleteJoint()
    {
        curModifiable = maxModifiable;
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
