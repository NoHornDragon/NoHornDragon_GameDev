using NHD.Entity.Yeouiju;
using NHD.StaticData.Settings;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.Entity.Player
{
    public class PlayerGrapher : MonoBehaviour
    {
        // this event = YeouijuLaunch.disJointEvent
        public event Action DeleteJointEvent;
        [SerializeField] private float _lineModifySpeed;
        private LineRenderer _lineRenderer;
        private DistanceJoint2D _joint;

        [Header("여의주 상태")]
        private bool _nowJoint;
        private bool _easyMode;
        [SerializeField] private float _minDistance;
        [SerializeField] private float _jointMaxTime;
        private float _jointTimer;
        [SerializeField] private float _canModifyAmount;
        private float _nowModify;

        [Header("여의주 HUD")]
        [SerializeField] private GameObject _playerHUD;
        [SerializeField] private Image _coolTimeImage;
        [SerializeField] private Image _modifyAmountImage;


        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _joint = GetComponent<DistanceJoint2D>();
            
            _easyMode = !StaticSettingsData._isHardMode;

            SetHUDInitial();
            _lineModifySpeed *= -1;

            // Set DistanceJoint2D's anchor to player
            _joint.anchor = Vector3.zero;
            SetLine(false);

            FindObjectOfType<YeouijuLaunch>().DisJointEvent += DeleteJoint;
            FindObjectOfType<YeouijuReflection>().CollisionEvent += MakeJoint;

            // if player using _easymode, make limit null and don't sub delegate
            if (_easyMode)
            {
                _coolTimeImage = null;
                _playerHUD = null;
                _modifyAmountImage = null;
                return;
            }
            // another limit is only in hard mode
            FindObjectOfType<YeouijuLaunch>().DisJointEvent += SetHUDInitial;
            FindObjectOfType<YeouijuReflection>().CollisionEvent += ActiveUI;
        }

        void Update()
        {
            if (!_nowJoint)
            {
                SetLine(false);
                _jointTimer = _jointMaxTime;
                _nowModify = 0;
                if (!_easyMode)
                    SetHUDInitial();
                return;
            }

            if (_joint.distance < _minDistance)
                DeleteJointEvent?.Invoke();

            _lineRenderer.SetPosition(1, this.transform.position);



            // modify grapher line
            float inputModify = Input.GetAxis("Vertical") * _lineModifySpeed;
            if (inputModify != 0)
                ModifyLine(inputModify);

            if (_easyMode) return;
            // if game is hard mode, Timer is on
            _jointTimer -= Time.deltaTime;
            _coolTimeImage.fillAmount = _jointTimer / _jointMaxTime;

            if (_jointTimer < 0)
            {
                _nowJoint = false;
                DeleteJointEvent?.Invoke();
            }

        }


        // 
        void ModifyLine(float amount)
        {
            // if all use modify amount, player can't modify line
            if (_nowModify > _canModifyAmount) return;

            // modify yeouiju line
            _joint.distance += amount;

            if (_easyMode) return;

            // hard mode have modify limit
            _nowModify += Mathf.Abs(amount);

            // set hud 
            _modifyAmountImage.fillAmount = (_canModifyAmount - _nowModify) / _canModifyAmount;
        }

        public bool NowJoint()
        {
            return _nowJoint;
        }

        public void MakeJoint(Vector2 target)
        {
            _nowJoint = true;
            // set joint position
            _joint.connectedAnchor = target;

            // set line renderer
            _lineRenderer.SetPosition(0, target);
            _lineRenderer.SetPosition(1, this.transform.position);

            SetLine(true);
        }

        // active player HUDs. only in hardmode
        public void ActiveUI(Vector2 dummy)
        {
            _playerHUD.SetActive(true);
        }

        public void DeleteJoint()
        {
            _nowJoint = false;
            SetLine(false);
        }

        void SetLine(bool active)
        {
            _lineRenderer.enabled = active;
            _joint.enabled = active;
        }

        // return HUD UI to initial
        private void SetHUDInitial()
        {
            _coolTimeImage.fillAmount = 1;
            _modifyAmountImage.fillAmount = 1;

            _playerHUD.SetActive(false);
        }
    }
}