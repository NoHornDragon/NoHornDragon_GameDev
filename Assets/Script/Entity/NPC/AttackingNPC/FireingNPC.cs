using NHD.GamePlay.InteractionEntity.FiringObject;
using System.Collections;
using UnityEngine;
using NHD.GamePlay.ObjectPool;

namespace NHD.Entity.NPC.AttackingNPC
{
    public class FireingNPC : MonoBehaviour
    {
        private IObjectPool _firePool;
        private Transform _player;
        [SerializeField] private Transform _bone;
        private bool _lookAtRight;
        [SerializeField] private float _launchTime;
        private float _curTime;


        private void Awake()
        {
            _firePool = GetComponent<IObjectPool>();
            // TODO : in multiplayer
            _player = GameObject.FindWithTag("Player").transform;

            BoxCollider2D collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(StartAimingPlayer());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StopAllCoroutines();
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator StartAimingPlayer()
        {
            while (true)
            {
                _curTime += Time.deltaTime;
                if (_curTime >= _launchTime)
                {
                    _firePool.GetObjectFromPool();
                    _curTime = 0;
                }


                var len = _player.position - transform.position;
                // TODO : sprite.splitx
                if ((len.x > 0) != _lookAtRight)
                {
                    _lookAtRight = (len.x > 0);
                    transform.localScale = (_lookAtRight) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
                }

                float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, -50, 50);
                _bone.localRotation = Quaternion.Euler(0, 0, angle);

                yield return null;
            }

        }
    }
}