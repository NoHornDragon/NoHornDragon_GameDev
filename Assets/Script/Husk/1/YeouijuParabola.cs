using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YeouijuParabola : MonoBehaviour
{
    public event Action DeleteJoint2D;
    [SerializeField] YeouijuMoving yeouiju;
    private Vector2 len;
    public float launchPower = 0;
    public float maxPower;
    public bool launching;

    private void Update() 
    {
        // 마우스 방향에 따라 오브젝트의 회전각 결정
        len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);



        // Launching Yeouiju
        if(Input.GetKeyDown(KeyCode.Space))
            launching = !launching;

        if(!launching)
        {
            DeleteJoint2D();
            return;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            launchPower = (launchPower > maxPower) ? maxPower : (launchPower + Time.deltaTime *5f);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {   
            yeouiju.LaunchYeouiju(z, launchPower);

            launchPower = 0f;
        }


    }


}
