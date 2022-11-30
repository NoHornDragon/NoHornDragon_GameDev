using NHD.Entity.Enemy.Common;
using NHD.Entity.Player;
using NHD.Sound.Common;
using NHD.Utils.SoundUtil;
using System.Collections;
using UnityEngine;

namespace NHD.Entity.Enemy.crab
{
    public class Crab : SoundPlayableBase, IEnemy
    {
        [SerializeField] private float ATTACK_DELAY_SEC = 3.0f;
        [SerializeField] private float REFLECT_POWER = 300.0f;
        private EnemyState _state;
        private bool _isAttackable;
        private Collider2D _collider;
        private WaitForSeconds _attackDelay;
        private Transform _player;

        void Start()
        {
            _isAttackable = true;
            _collider = GetComponent<Collider2D>();
            _attackDelay = new WaitForSeconds(ATTACK_DELAY_SEC);
        }

        void Update()
        {
            CheckAttackable();
        }

        public void CheckAttackable()
        {
            if (_state == EnemyState.ATTACK && _isAttackable)
            {
                Attack();
            }
        }

        public void Attack()
        {
            _isAttackable = false;
            StartCoroutine(AttackCoroutine());
        }

        IEnumerator AttackCoroutine()
        {
            Rigidbody2D playerRigid = _player.GetComponent<Rigidbody2D>();
            Vector2 reflectDir = _player.position - _collider.bounds.center;

            reflectDir = reflectDir.normalized;
            playerRigid.velocity = Vector2.zero;
            playerRigid.AddForce(reflectDir * REFLECT_POWER);
            SoundManager._instance.PlayEFXAtPoint(_audioClips[0], transform.position);
            _player.GetComponent<PlayerCollider>().PlayerStunEvent();

            yield return _attackDelay;

            _isAttackable = true;
        }

        public void Move() { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _player = collision.transform;
                _state = EnemyState.ATTACK;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _player = null;
                _state = EnemyState.IDLE;
            }
        }

    }
}