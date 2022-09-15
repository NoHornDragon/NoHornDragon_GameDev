using System;
using UnityEngine;

namespace NHD.Entity.Player
{
    public class PlayerCollider : MonoBehaviour
    {
        public event Action<bool> playerStunEvent;
        public event Action<bool> playerChangeEvent;
        private PlayerMovement player;
        private GameObject playerVisual;
        private bool isOrigin = true;
        private AnotherMovement anotherMovement;
        void Start()
        {
            player = GetComponent<PlayerMovement>();
            playerVisual = transform.GetChild(0).gameObject;

            playerChangeEvent += (bool input) => { playerVisual.SetActive(input); };

            FindObjectOfType<PlayerMovement>().playerResetEvent += SetPlayerOrigin;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                //TODO :  another movement일때 처리 동작
                // if now not origin movement, first change to origin player
                PlayerChanged(true, null);

                PlayerStunEvent();
            }
            if (other.CompareTag("AnotherMovement"))
            {
                // change player
                PlayerChanged(!isOrigin, other.gameObject.GetComponent<AnotherMovement>());
            }
        }

        private void FixedUpdate()
        {
            if (isOrigin) return;
            if (anotherMovement == null) return;

            // if now playing with no originmovement, player's collider will follow anothermovement
            this.transform.position = anotherMovement.transform.position;
        }

        private void PlayerChanged(bool isOrigin, AnotherMovement newPlayer = null)
        {

            this.isOrigin = isOrigin;
            anotherMovement = (isOrigin) ? null : newPlayer;

            playerChangeEvent?.Invoke(isOrigin);

        }

        private void SetPlayerOrigin()
        {
            PlayerChanged(true, null);
        }

        public void PlayerStunEvent()
        {
            if (player.stuned) return;

            playerStunEvent?.Invoke(true);
        }
    }
}