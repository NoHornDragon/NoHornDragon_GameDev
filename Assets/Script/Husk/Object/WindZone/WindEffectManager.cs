using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectManager : MonoBehaviour
{
    [SerializeField] WindZone wind;
    [SerializeField] AreaEffector2D effector;
    [SerializeField] ParticleSystem particle;
    
    public float timerTime;
    private float timer;
    bool nowWindy = true;
    [Header("이펙터 항목")]
    [SerializeField] private float windPower;
    [Header("파티클 항목")]
    [SerializeField] private float xSpeed;

    void Start()
    {
        timer = timerTime;
    }

    void Update()
    {
        if(timer > 0){
            timer -= Time.deltaTime;
            return;
        }

        timer = timerTime;
        if(nowWindy)
            StopWind();
        else
            StartWind();
    }

    void StartWind()
    {
        // physic effect


        nowWindy = true;
    }

    void StopWind()
    {
        nowWindy = false;
    }
}
