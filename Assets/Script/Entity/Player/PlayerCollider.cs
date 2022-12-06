using System;
using UnityEngine;

namespace NHD.Entity.Player
{
    public class PlayerCollider : MonoBehaviour
    {
        public event Action<bool> PlayerStunEvent;
        public event Action<bool> PlayerChangeEvent;
        private PlayerMovement _player;
        private GameObject _playerVisual;
        private bool _isOrigin = true;
        private AnotherMovement _anotherMovement;
        void Start()
        {
            _player = GetComponent<PlayerMovement>();
            _playerVisual = transform.GetChild(0).gameObject;

            PlayerChangeEvent += (bool input) => { _playerVisual.SetActive(input); };

            FindObjectOfType<PlayerMovement>().PlayerResetEvent += SetPlayerOrigin;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                //TODO :  another movement일때 처리 동작
                // if now not origin movement, first change to origin player
                PlayerChanged(true, null);

                TriggerPlayerStunEvent();
            }
            if (other.CompareTag("AnotherMovement"))
            {
                // change player
                PlayerChanged(!_isOrigin, other.gameObject.GetComponent<AnotherMovement>());
            }
        }

        private void FixedUpdate()
        {
            if (_isOrigin) return;
            if (_anotherMovement == null) return;

            // if now playing with no originmovement, player's collider will follow anothermovement
            this.transform.position = _anotherMovement.transform.position;
        }

        private void PlayerChanged(bool isOrigin, AnotherMovement newPlayer = null)
        {

            this._isOrigin = isOrigin;
            _anotherMovement = (isOrigin) ? null : newPlayer;

            PlayerChangeEvent?.Invoke(isOrigin);

        }

        private void SetPlayerOrigin()
        {
            PlayerChanged(true, null);
        }

        public void TriggerPlayerStunEvent()
        {
            if (_player._stuned) return;

            PlayerStunEvent?.Invoke(true);
        }
    }
}