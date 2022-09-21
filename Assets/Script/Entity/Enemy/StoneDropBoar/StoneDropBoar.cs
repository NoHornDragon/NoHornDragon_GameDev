using NHD.Entity.Enemy.Common;
using System.Collections;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class StoneDropBoar : MonoBehaviour, IEnemy
    {
        private const int ATTACK_DELAY_SEC = 5;

        [SerializeField] private StonesPool _stonesPool;
        private GameObject _stone;
        private Vector3 _instantStonePos;
        private EnemyState _state;
        private bool _isAttackAble;
        private WaitForSeconds _attackDelay;

        void Start()
        {
            _state = EnemyState.IDLE;
            _isAttackAble = true;
            _attackDelay = new WaitForSeconds(ATTACK_DELAY_SEC);
            _instantStonePos = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
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
            StartCoroutine(AttackCoroutine());
        }

        IEnumerator AttackCoroutine()
        {
            _stone = _stonesPool.GetObject().gameObject;
            _stone.transform.SetParent(transform);
            _stone.transform.position = _instantStonePos;
            _stone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 1000f));
            yield return _attackDelay;
            _isAttackAble = true;
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
