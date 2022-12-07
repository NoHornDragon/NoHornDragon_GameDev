using NHD.Entity.Player;
using System;
using UnityEngine;

namespace NHD.Entity.Yeouiju
{
    public class YeouijuReflection : MonoBehaviour
    {
        public event Action<Vector2> CollisionEvent;
        public event Action YeouijuReturnEvent;
        private Rigidbody2D _rigid;
        private CircleCollider2D _coll;
        private Transform _player;
        private SpriteRenderer _sprite;
        [SerializeField] private float _yeouijuSpeed;
        private bool _yeouijuOn;
        private Vector3 _prevVelocity;

        [Header("충돌 변수들")]
        [SerializeField] private int _collisionCount;
        [SerializeField] private float _maxDistance;
        [SerializeField] private int _reflectCount;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rigid = GetComponent<Rigidbody2D>();
            _coll = GetComponent<CircleCollider2D>();
            _sprite = GetComponent<SpriteRenderer>();

            FindObjectOfType<YeouijuLaunch>().DisJointEvent += YeouijuFollowPlayer;
            FindObjectOfType<PlayerCollider>().PlayerChangeEvent += SetYeouijuSprite;
            _coll.enabled = false;
        }

        private void FixedUpdate()
        {
            if (_yeouijuOn)
            {
                _prevVelocity = _rigid.velocity;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _player.position, _yeouijuSpeed * 3f * Time.deltaTime);
        }

        public void Launched(Vector3 position, float rotation)
        {
            _reflectCount = 0;

            _yeouijuOn = true;
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(0, 0, rotation);

            _rigid.velocity = transform.right * _yeouijuSpeed;
            _coll.enabled = true;
        }

        public void YeouijuFollowPlayer()
        {
            _yeouijuOn = false;
            _coll.enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_reflectCount < _collisionCount)
            {
                _reflectCount++;

                float speed = _prevVelocity.magnitude;
                Vector2 Direction = Vector2.Reflect(_prevVelocity.normalized, other.contacts[0].normal);

                _rigid.velocity = Direction * Mathf.Max(speed, 0f);
                return;
            }

            if (Vector2.Distance(_player.position, this.transform.position) > _maxDistance)
            {
                _reflectCount = 0;

                YeouijuReturnEvent?.Invoke();

                YeouijuFollowPlayer();
                _rigid.freezeRotation = true;
                return;
            }

            CollisionEvent?.Invoke(this.transform.position);

            _rigid.velocity = new Vector3(0, 0, 0);
            _rigid.freezeRotation = true;
        }

        public void SetYeouijuSprite(bool isActive)
        {
            _sprite.enabled = isActive;
        }
    }
}