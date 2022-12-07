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
        private Collider2D _collider;
        private Transform _player;

        void Start()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void Attack()
        {
            SoundManager._instance.PlayEFXAtPoint(_audioClips[0], transform.position);
            _player.GetComponent<PlayerCollider>().TriggerPlayerStunEvent(true, _collider.bounds.center, REFLECT_POWER);
        }

        public void Move() { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _player = collision.transform;
                Attack();
            }
        }
    }
}