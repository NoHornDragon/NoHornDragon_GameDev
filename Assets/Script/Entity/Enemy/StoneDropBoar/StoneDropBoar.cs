using NHD.Entity.Enemy.Common;
using System.Collections;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class StoneDropBoar : MonoBehaviour, IEnemy
    {
        private const int ATTACK_DELAY = 5;

        [SerializeField] private StonesPool _stonesPool;
        private GameObject _stone;
        private Vector3 _instantStonePos;
        private EnemyState _state;
        private bool _attackAble;

        void Start()
        {
            _state = EnemyState.IDLE;
            _attackAble = true;
            _instantStonePos = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
        }

        void Update()
        {
            CheckAttackable();
        }

        public void CheckAttackable()
        {
            if (_state == EnemyState.ATTACK && _attackAble)
                Attack();
        }

        public void Attack()
        {
            _attackAble = false;
            StartCoroutine(AttackCoroutine());
        }

        IEnumerator AttackCoroutine()
        {
            _stone = _stonesPool.InstantiateStone();
            _stone.transform.position = _instantStonePos;
            _stone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            _stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 1000f));
            yield return new WaitForSeconds(ATTACK_DELAY);
            _attackAble = true;
        }

        public void Move() { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                _state = EnemyState.ATTACK;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                _state = EnemyState.IDLE;
        }
    }
}
