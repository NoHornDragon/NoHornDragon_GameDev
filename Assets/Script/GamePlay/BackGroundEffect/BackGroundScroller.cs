using System;
using UnityEngine;

namespace NHD.GamePlay.BackGroundEffect
{

    public class BackGroundScroller : MonoBehaviour
    {
        public event Action<float, float> _playerMoveEvent;
        private Vector2 _initialPos;
        private Vector2 _prevPos;
        public Transform _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _initialPos = this.transform.position;
        }

        private void Start()
        {
            _prevPos = transform.position;
        }

        private void Update()
        {
            _playerMoveEvent?.Invoke(_initialPos.x - transform.position.x, _initialPos.y - transform.position.y);
        }

        public void ChangeCameraPos(int d, bool input)
        {
            // if(!input)  return;

            _initialPos = _player.position;
        }
    }

}