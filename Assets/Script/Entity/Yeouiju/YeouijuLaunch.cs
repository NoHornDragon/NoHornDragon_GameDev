using NHD.Entity.Player;
using NHD.StaticData.Settings;
using System;
using System.Collections;
using UnityEngine;

namespace NHD.Entity.Yeouiju
{
    public class YeouijuLaunch : MonoBehaviour
    {
        public event Action DisJointEvent;
        private YeouijuReflection _yeouiju;
        private LineRenderer _predictionLine;
        private int _predictionLayerMask;
        private RaycastHit2D _predictionHit;
        [SerializeField] private bool _canLaunch;
        [SerializeField] private bool _prepareYeouiju;
        private bool _isYeouijuOn;
        private bool _usingEasyMode;
        private Vector2 _shouldDrawPoint;

        private void Start()
        {
            _predictionLayerMask = (1 << LayerMask.NameToLayer("Ground"));

            _canLaunch = true;

            _usingEasyMode = !StaticSettingsData._isHardMode;

            _yeouiju = FindObjectOfType<YeouijuReflection>();
            _predictionLine = GetComponent<LineRenderer>();
            _predictionLine.enabled = false;

            DisJointEvent += SetYeouijuFalse;

            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            playerMovement.PlayerRecoverEvent += SetLaunchStatus;
            playerMovement.PlayerResetEvent += ReturnYeouiju;

            PlayerCollider playerCollider = FindObjectOfType<PlayerCollider>();
            playerCollider.PlayerChangeEvent += SetLaunchStatus;
            playerCollider.PlayerStunEvent += StunedYeouiju;

            FindObjectOfType<YeouijuReflection>().YeouijuReturnEvent += ReturnYeouiju;
            FindObjectOfType<PlayerGrapher>().DeleteJointEvent += ReturnYeouiju;
        }

        private void Update()
        {
            if (!_canLaunch) return;

            if (Input.GetMouseButtonDown(0))
            {
                _prepareYeouiju = true;
            }

            // calculate angle by mouse pointer
            var len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, z);

            if (!_isYeouijuOn && _prepareYeouiju)
            {
                DrawPredictionLine();
            }

            if (!Input.GetMouseButtonUp(0)) return;

            // By button up, yeouiju will launched or returned
            if (!_isYeouijuOn)
            {
                _predictionLine.enabled = false;
                _isYeouijuOn = true;

                _yeouiju.Launched(this.transform.position, z);
                return;
            }

            ReturnYeouiju();
        }

        private void DrawPredictionLine()
        {
            _predictionLine.SetPosition(0, this.transform.position);
            _predictionHit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, _predictionLayerMask);

            if (_predictionHit.collider == null)
            {
                _predictionLine.enabled = false;
                return;
            }

            _shouldDrawPoint = _predictionHit.point;
            _predictionLine.SetPosition(1, _shouldDrawPoint);

            var inDirection = (_predictionHit.point - (Vector2)transform.position).normalized;
            var reflectionDir = Vector2.Reflect(inDirection, _predictionHit.normal);

            _predictionHit = Physics2D.Raycast(_predictionHit.point + (reflectionDir * 0.001f), reflectionDir, Mathf.Infinity, _predictionLayerMask);

            if (_predictionHit.collider == null)
            {
                _shouldDrawPoint = (Vector2)_predictionLine.GetPosition(1) + (reflectionDir * 15f);
            }
            else
            {
                _shouldDrawPoint = _predictionHit.point;
            }

            _predictionLine.SetPosition(2, _shouldDrawPoint);
            _predictionLine.enabled = true;
        }

        private void SetYeouijuFalse()
        {
            _isYeouijuOn = false;
            _prepareYeouiju = false;
        }

        public void SetLaunchStatus(bool isActive)
        {
            StartCoroutine(SetYeouijuStatueCourtine(isActive));
        }

        public void StunedYeouiju(bool isStuned)
        {
            _canLaunch = !isStuned;
            _predictionLine.enabled = false;
            ReturnYeouiju();
        }

        private void ReturnYeouiju()
        {
            DisJointEvent?.Invoke();
        }

        IEnumerator SetYeouijuStatueCourtine(bool isActive)
        {
            yield return null;
            this._canLaunch = isActive;
        }

    }
}