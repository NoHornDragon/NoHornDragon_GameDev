using NHD.Entity.Enemy.Common;
using NHD.Utils.SoundUtil;
using System.Collections;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class StoneDropBoar : MonoBehaviour, IEnemy
    {
        private const float WAIT_ATTACK = 0.5f;
        private const float ATTACK_DELAY_SEC = 4.5f;

        private StonesPool _stonesPool;
        private EnemyState _state;
        private GameObject _stone;
        private Vector3 _instantStonePos;
        private bool _isAttackAble;
        private WaitForSeconds _waitAttack;
        private WaitForSeconds _attackDelay;
        private Animator _animator;

        void Start()
        {
            _stonesPool = FindObjectOfType<StonesPool>();
            _state = EnemyState.IDLE;
            _isAttackAble = true;
            _waitAttack = new WaitForSeconds(WAIT_ATTACK);
            _attackDelay = new WaitForSeconds(ATTACK_DELAY_SEC);
            _instantStonePos = transform.position + new Vector3(-1.75f, 1, 0);
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            CheckAttackable();
        }

        public void CheckAttackable()
        {
            if (_state == EnemyState.ATTACK && _isAttackAble)
                Attack();
        }

        public void Attack()
        {
            _isAttackAble = false;
            _animator.Play("Boar_RollingMotion");
            _animator.SetTrigger("GoToIDLE");
            StartCoroutine(AttackCoroutine());
        }

        IEnumerator AttackCoroutine()
        {
            _stone = CreateStone();
            yield return _waitAttack;

            SoundManager._instance.PlayEFX("Effect2", transform.position);
            _stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 1000f));
            yield return _attackDelay;

            _isAttackAble = true;
        }

        private GameObject CreateStone()
        {
            var stone = _stonesPool.GetObjectFromPool().gameObject;
            stone.transform.SetParent(transform);
            stone.transform.position = _instantStonePos;
            stone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            return stone;
        }

        public void Move() { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                _state = EnemyState.ATTACK;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                _state = EnemyState.IDLE;
        }
    }
}
