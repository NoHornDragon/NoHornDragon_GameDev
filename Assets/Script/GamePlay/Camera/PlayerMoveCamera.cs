using NHD.Entity.Player;
using UnityEngine;

namespace NHD.GamePlay.Camera
{
    public class PlayerMoveCamera : MonoBehaviour
    {
        private PlayerMovement _player;
        [SerializeField] private float _cameraMoveAmount;
        [SerializeField] [Range(0, 0.3f)]
        private float _cameraMoveSpeed;
        [SerializeField] private Vector3 _nowInput = new Vector3(0, 0, 0);

        private void Start()
        {
            _player = transform.parent.GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (!_player.onGround || _player.nowJoint)
            {
                CameraReturnToPlayer();
                return;
            }

            _nowInput.x = Input.GetAxis("Horizontal");
            _nowInput.y = Input.GetAxis("Vertical");

            if (_nowInput.x == 0 && _nowInput.y == 0)
            {
                CameraReturnToPlayer();
                return;
            }

            if (Mathf.Abs(transform.localPosition.x) > _cameraMoveAmount)
                _nowInput.x = 0;
            if (Mathf.Abs(transform.localPosition.y) > _cameraMoveAmount)
                _nowInput.y = 0;
                
            this.transform.localPosition += _nowInput * _cameraMoveSpeed;
        }

        private void CameraReturnToPlayer()
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, _cameraMoveSpeed);
        }
    }
}