using NHD.Algorithm.PathFinding;
using System.Collections;
using UnityEngine;
using NHD.GamePlay.ObjectPool;

namespace NHD.GamePlay.InteractionEntity.FiringObject
{
    public class Arrow : PoolableObjectBase
    {
        public Transform _target;
        public float _speed = 5.0f;
        [SerializeField] Vector2[] _path;
        [SerializeField] private int _pathIndex;
        [SerializeField] private Vector2 _moveDir;

        private void Awake()
        {
            _target = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable()
        {
            if (RequestAStarPath.instance == null) return;

            RequestAStarPath.RequestPath(transform.position, _target.position, AfterFindPath);
        }

        private void AfterFindPath(Vector2[] way, bool success)
        {
            if (!success) return;

            _path = way;

            StopAllCoroutines();

            if (this.gameObject.activeInHierarchy)
                StartCoroutine("MoveToTarget");
        }


        IEnumerator MoveToTarget()
        {
            Vector2 curPos = _path[0];
            _moveDir = curPos - (Vector2)transform.position;
            transform.right = _moveDir;

            while (true)
            {
                if ((Vector2)transform.position == curPos)
                {
                    _pathIndex++;
                    if (_pathIndex >= _path.Length)
                    {
                        StartCoroutine(MoveToEndPoint());
                        yield break;
                    }
                    curPos = _path[_pathIndex];
                    _moveDir = curPos - (Vector2)transform.position;
                    transform.right = _moveDir;
                }

                transform.position = Vector2.MoveTowards(transform.position, curPos, _speed * Time.deltaTime);
                yield return null;
            }

        }

        IEnumerator MoveToEndPoint()
        {
            while (true)
            {
                transform.position = Vector2.MoveTowards(transform.position, _moveDir * 100, _speed * Time.deltaTime);
                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Player":
                    StopCoroutine("MoveToTarget");
                    InvokeReturnCall();
                    break;
                case "Ground":
                    StopCoroutine("MoveToTarget");
                    InvokeReturnCall();
                    break;
            }
        }


        private void OnDisable()
        {
            _pathIndex = 0;
        }
    }

}