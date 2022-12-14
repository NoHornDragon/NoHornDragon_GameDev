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
        [SerializeField] private float REFLECT_POWER = 300.0f;
        [SerializeField] private float ATTACK_DELAY = 5.0f;
        private bool _isAttackable;
        private Collider2D _collider;
        private Transform _player;
        private WaitForSeconds _attackDelay;

        void Start()
        {
            _isAttackable = true;
            _collider = GetComponent<Collider2D>();
            _attackDelay = new WaitForSeconds(ATTACK_DELAY);
        }

        public void Attack()
        {
            SoundManager._instance.PlayEFXAtPoint(_audioClips[0], transform.position);
            _player.GetComponent<PlayerCollider>().TriggerPlayerStunEvent(true, _collider.bounds.center, REFLECT_POWER);
        }

        IEnumerator AttackCoroutine()
        {
            _isAttackable = false;
            Attack();
            yield return _attackDelay;
            _isAttackable = true;
        }

        public void Move() { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _player = collision.transform;
                if(_isAttackable)
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
    }
}