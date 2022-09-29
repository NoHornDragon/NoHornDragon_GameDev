using System.Collections;
using UnityEngine;

namespace NHD.GamePlay.InteractionEntity.WindZone
{
    public class WindEffectManager : MonoBehaviour
    {
        [SerializeField] GameObject windZone;
        [SerializeField] AreaEffector2D effector;
        [SerializeField] ParticleSystem particle;

        [Header("타이머 시간")]
        [SerializeField] private float windTime;
        [SerializeField] private float idleTime;
        private float timer;
        private bool nowWindy;

        [Header("이펙터 항목")]
        [SerializeField] private float windPower;

        void Start()
        {
            nowWindy = false;
            timer = idleTime;

            windZone = transform.GetChild(1).gameObject;

            windZone.transform.localEulerAngles = GetWindAngle(effector.forceAngle);
            windZone.SetActive(false);
        }

        void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return;
            }

            timer = nowWindy ? idleTime : windTime;

            if (nowWindy)
            {
                StartCoroutine(StopWind());
            }
            else
            {
                StartCoroutine(StartWind());
            }
        }

        WaitForSeconds waitTime = new WaitForSeconds(1f);
        IEnumerator StartWind()
        {
            windZone.SetActive(true);

            yield return waitTime;

            effector.forceMagnitude = windPower;

            nowWindy = true;
        }

        IEnumerator StopWind()
        {
            windZone.SetActive(false);

            yield return waitTime;

            effector.forceMagnitude = 0;

            nowWindy = false;
        }

        Vector3 GetWindAngle(float effectorDir)
        {
            /*
                effector의 방향은 0부터 시작해 90도마다 오른쪽, 위, 왼쪽, 아래로 이동합니다. 0, 90, 180, 270
                파티클 이펙트를 주는 windZone의 경우 오른쪽, 위, 왼쪽, 아래 순으로 벡터(x, y)를 나열해 보면

                - 오른쪽 : 0, 90                => 0
                - 위 : -90, 0 or 270, 0         => 90
                - 왼쪽 : 0, 270 or 0, -90       => 180
                - 아래 : 90, 0                  => 270
            */
            Vector3 angle = Vector3.zero;

            switch (effectorDir)
            {
                case 0:
                    angle = new Vector3(0, 90, 0);
                    break;

                case 90:
                    angle = new Vector3(270, 0, 0);
                    break;

                case 180:
                    angle = new Vector3(0, 270, 0);
                    break;

                case 270:
                    angle = new Vector3(90, 0, 0);
                    break;
            }

            return angle;
        }
    }
}