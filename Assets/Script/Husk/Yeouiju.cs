using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Yeouiju : MonoBehaviour
{
    public event Action DisjointAction;
    [SerializeField] private GameObject yeouiju;
    [SerializeField] Rigidbody2D yeouijuRigid;
    public Transform launchPoint;
    public float yeouijuSpeed;
    public bool isYeouijuOn;
    Vector2 len;
    private void Start() 
    {

    }

    void Update()
    {   
        // 마우스 방향에 따라 오브젝트의 회전각 결정
        len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);

        if(Input.GetMouseButtonDown(1))
        {
            // 발사 강도 정하기
            if(!isYeouijuOn)
            {
                isYeouijuOn = true;
                yeouiju.SetActive(true);
                yeouiju.transform.position = this.transform.position;
                yeouiju.transform.rotation = Quaternion.Euler(0, 0, z);

                yeouijuRigid.velocity = launchPoint.right * yeouijuSpeed;

                //StartCoroutine(ReturnYeouiju(5f));
            }
            else 
            {
                // StartCoroutine(ReturnYeouiju(0));
                isYeouijuOn = false;

                if(DisjointAction != null)
                    DisjointAction();
            }
        }


    }

    IEnumerator ReturnYeouiju(float time)
    {   
        //TODO : 발사 -> 취소 -> 발사 시에도 돌아오는 경우 해결
        yield return new WaitForSeconds(time);
        

        isYeouijuOn = false;
    }
}