using System.Collections;
using UnityEngine;

namespace NHD.GamePlay.InteractionEntity.WindZone
{
    public class WindEffectManager : MonoBehaviour
    {
        [SerializeField] GameObject _windZone;
        [SerializeField] AreaEffector2D _effector;

        [Header("타이머 시간")]
        [SerializeField] private float _windTime;
        [SerializeField] private float _idleTime;
        private float _timer;
        private bool _nowWindy;

        [Header("이펙터 항목")]
        [SerializeField] private float _windPower;

        void Start()
        {
            _timer = _idleTime;

            _windZone = transform.GetChild(1).gameObject;
            _effector = GetComponentInChildren<AreaEffector2D>();

            _windZone.transform.localEulerAngles = GetWindAngle(_effector.forceAngle);
            _windZone.SetActive(false);
        }

        void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }

            _timer = _nowWindy ? _idleTime : _windTime;

            if (_nowWindy)
            {
                StartCoroutine(StopWind());
            }
            else
            {
                StartCoroutine(StartWind());
            }
        }

        WaitForSeconds _waitTime = new WaitForSeconds(1f);
        IEnumerator StartWind()
        {
            _windZone.SetActive(true);

            yield return _waitTime;

            _effector.forceMagnitude = _windPower;

            _nowWindy = true;
        }

        IEnumerator StopWind()
        {
            _windZone.SetActive(false);

            yield return _waitTime;

            _effector.forceMagnitude = 0;

            _nowWindy = false;
        }

        Vector3 GetWindAngle(float effectorDir)
        {
            Vector3 angle = new Vector3(0, 0, 0);

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