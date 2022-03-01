using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Yeouiju : MonoBehaviour
{
    public event Action DisjointAction;
    [SerializeField] private GameObject yeouiju;
    [SerializeField] Rigidbody2D yeouijuRigid;
    public float yeouijuSpeed;
    public bool isYeouijuOn;
    private void Start() 
    {
        yeouiju = GameObject.Find("Yeouiju");
        yeouijuRigid = yeouiju.GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        // 마우스 방향에 따라 오브젝트의 회전각 결정
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);

        if(!Input.GetMouseButtonUp(0))
            return;
        
        // 발사 강도 정하기
        if(!isYeouijuOn)
        {
            isYeouijuOn = true;
            yeouiju.transform.position = this.transform.position;
            yeouiju.transform.rotation = Quaternion.Euler(0, 0, z);

            yeouijuRigid.velocity = yeouiju.transform.right * yeouijuSpeed;
        }
        else 
        {
            Disjoint();
        }
    }

    public void Disjoint()
    {
        isYeouijuOn = false;
        if(DisjointAction != null)
            DisjointAction();
    }
}