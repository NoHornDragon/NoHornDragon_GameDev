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
        public event Action deleteJointEvent;
        [SerializeField] private float lineModifySpeed;
        private LineRenderer lineRenderer;
        private DistanceJoint2D joint;

        [Header("여의주 상태")]
        private bool nowJoint;
        private bool easyMode;
        [SerializeField] private float minDistance;
        [SerializeField] private float jointMaxTime;
        private float jointTimer;
        [SerializeField] private float canModifyAmount;
        private float nowModify;

        [Header("여의주 HUD")]
        [SerializeField] private GameObject playerHUD;
        [SerializeField] private Image coolTimeImage;
        [SerializeField] private Image modifyAmountImage;


        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            joint = GetComponent<DistanceJoint2D>();
            
            easyMode = !StaticSettingsData._isHardMode;

            SetHUDInitial();
            lineModifySpeed *= -1;

            // Set DistanceJoint2D's anchor to player
            joint.anchor = Vector3.zero;
            SetLine(false);

            FindObjectOfType<YeouijuLaunch>().disJointEvent += DeleteJoint;
            FindObjectOfType<YeouijuReflection>().collisionEvent += MakeJoint;

            // if player using easymode, make limit null and don't sub delegate
            if (easyMode)
            {
                coolTimeImage = null;
                playerHUD = null;
                modifyAmountImage = null;
                return;
            }
            // another limit is only in hard mode
            FindObjectOfType<YeouijuLaunch>().disJointEvent += SetHUDInitial;
            FindObjectOfType<YeouijuReflection>().collisionEvent += ActiveUI;
        }

        void Update()
        {
            if (!nowJoint)
            {
                SetLine(false);
                jointTimer = jointMaxTime;
                nowModify = 0;
                if (!easyMode)
                    SetHUDInitial();
                return;
            }

            if (joint.distance < minDistance)
                deleteJointEvent?.Invoke();

            lineRenderer.SetPosition(1, this.transform.position);



            // modify grapher line
            float inputModify = Input.GetAxis("Vertical") * lineModifySpeed;
            if (inputModify != 0)
                ModifyLine(inputModify);

            if (easyMode) return;
            // if game is hard mode, Timer is on
            jointTimer -= Time.deltaTime;
            coolTimeImage.fillAmount = jointTimer / jointMaxTime;

            if (jointTimer < 0)
            {
                nowJoint = false;
                deleteJointEvent?.Invoke();
            }

        }


        // 
        void ModifyLine(float amount)
        {
            // if all use modify amount, player can't modify line
            if (nowModify > canModifyAmount) return;

            // modify yeouiju line
            joint.distance += amount;

            if (easyMode) return;

            // hard mode have modify limit
            nowModify += Mathf.Abs(amount);

            // set hud 
            modifyAmountImage.fillAmount = (canModifyAmount - nowModify) / canModifyAmount;
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

        // active player HUDs. only in hardmode
        public void ActiveUI(Vector2 dummy)
        {
            playerHUD.SetActive(true);
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

        // return HUD UI to initial
        private void SetHUDInitial()
        {
            coolTimeImage.fillAmount = 1;
            modifyAmountImage.fillAmount = 1;

            playerHUD.SetActive(false);
        }
    }
}