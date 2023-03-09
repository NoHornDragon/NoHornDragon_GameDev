using NHD.DataController.Savers;
using NHD.Entity.Yeouiju;
using NHD.Sound.Common;
using NHD.StaticData.History;
using NHD.StaticData.Settings;
using NHD.Utils.SoundUtil;
using System;
using System.Collections;
using UnityEngine;

namespace NHD.Entity.Player
{
    public class PlayerMovement : SoundPlayableBase
    {
        public event Action<bool> PlayerRecoverEvent;
        public event Action<bool> PlayerPositionResetEvent;
        public event Action PlayerResetEvent;
        // another component
        private Rigidbody2D _rigid;
        private PlayerGrapher _grapher;
        private PlayerCollider _coll;

        [Header("플레이어 능력치")]
        private float _horizontalSpeed;
        [SerializeField] private float _swingPower;

        [Header("플레이어 현재 상태")]
        private bool _usingEasyMode;
        [SerializeField] private bool _canMove;

        [Space(20f)]
        public bool _nowJoint;
        public bool _throwed;
        public bool _prepareLaunch;
        public bool _throwYeouiju;

        [Space(20f)]
        public bool _stuned;
        [SerializeField] private float _stunedTime;
        private float _stunTimer;

        [Space(20f)]

        public bool _onGround;
        [SerializeField] private Vector2 _bottomOffset;
        [SerializeField] private float _collisionRadius;
        [SerializeField] private LayerMask _groundLayer;


        void Start()
        {
            _canMove = true;

            _rigid = GetComponent<Rigidbody2D>();
            _grapher = GetComponent<PlayerGrapher>();
            _coll = GetComponent<PlayerCollider>();

            FindObjectOfType<YeouijuReflection>().CollisionEvent += MakeJoint;
            _coll.PlayerStunEvent += PlayerStuned;
            _coll.PlayerChangeEvent += PlayerBecomeOrigin;
            FindObjectOfType<YeouijuLaunch>().DisJointEvent += DeleteJoint;

            _usingEasyMode = !StaticSettingsData._isHardMode;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerReset();
                return;
            }

            _onGround = Physics2D.OverlapCircle((Vector2)transform.position + _bottomOffset, _collisionRadius, _groundLayer);

            if (!_onGround && !_nowJoint)
            {
                _stunTimer += Time.deltaTime;
                if (_stunTimer > _stunedTime)
                {
                    _coll.TriggerPlayerStunEvent();
                }
            }
            else
            {
                _stunTimer = 0;
            }

            if (!_canMove) return;

            _horizontalSpeed = Input.GetAxis("Horizontal");

            if (Input.GetMouseButtonDown(0))
            {
                _prepareLaunch = true;
            }
            if (Input.GetMouseButtonUp(0) && _prepareLaunch)
            {
                SoundManager._instance.PlayEFXAmbient(_audioClips[0]);
                _prepareLaunch = false;
                _throwYeouiju = true;
                _throwed = !_throwed;
            }
        }

        private void FixedUpdate()
        {
            if (_grapher.NowJoint() == false)
                return;

            _rigid.AddForce(Vector2.right * _horizontalSpeed * _swingPower);
        }

        private void PlayerReset()
        {
            PlayerResetEvent?.Invoke();

            this.gameObject.transform.position = Vector3.zero;

            PlayHistoryDataSaver.SaveData();

            PlayerPositionResetEvent?.Invoke(true);
        }

        private void MakeJoint(Vector2 dummyInput)
        {
            _nowJoint = true;
        }

        private void DeleteJoint()
        {
            _nowJoint = false;
            _prepareLaunch = false;
            _throwYeouiju = false;
            _throwed = false;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere((Vector2)transform.position + _bottomOffset, _collisionRadius);
        }

        public bool PlayerFlip()
        {
            return _rigid.velocity.x > 0;
        }

        public void PlayerStuned(bool isStuned)
        {
            _canMove = !isStuned;
            _stuned = isStuned;

            if(UnityEngine.Random.Range(0.0f, 1.0f) < 0.5f)
                SoundManager._instance.PlayRandomBGM();

            StaticHistoryData._stunCount += 1;
            PlayHistoryDataSaver.SaveData();

            StartCoroutine(PlayerRecoverFromStun());
        }

        public void PlayerKnockback(Vector3 fromPosition, float knockbackPower)
        {
            Vector2 reflectDir = transform.position - fromPosition;
            
            _rigid.velocity = Vector2.zero;
            _rigid.AddForce(reflectDir * knockbackPower);
        }

        WaitForSeconds stunRecoverCheck = new WaitForSeconds(2f);
        IEnumerator PlayerRecoverFromStun()
        {
            bool nowOnGround = _onGround;
            yield return stunRecoverCheck;

            if (nowOnGround && nowOnGround == _onGround)
            {
                _stuned = false;
                _canMove = true;

                PlayerRecoverEvent?.Invoke(true);
            }
            else
                StartCoroutine(PlayerRecoverFromStun());
        }

        public void PlayerBecomeOrigin(bool isOrigin)
        {
            _canMove = isOrigin;
            _rigid.gravityScale = (isOrigin) ? 1f : 0f;

        }
    }
}