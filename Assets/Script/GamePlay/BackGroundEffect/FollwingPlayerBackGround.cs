using UnityEngine;
using NHD.GamePlay.CameraComponent;

namespace NHD.GamePlay.BackGroundEffect
{
    public class FollwingPlayerBackGround : MonoBehaviour
    {
        private Transform _player;
        private Camera _mainCam;
        [SerializeField] private Vector2 _followingOffset;
        private bool _isActive;
        [SerializeField] private int _fromStageIndex;
        [SerializeField] private int _toStageIndex;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _mainCam = Camera.main;

            PlayerEnterConfiner[] confiners = FindObjectsOfType<PlayerEnterConfiner>();
            foreach (PlayerEnterConfiner confiner in confiners)
            {
                confiner.ActiveRoomEvent += SetBGFollow;
            }
        }

        private void Update()
        {
            if(!_isActive)   return;

            this.transform.position = (Vector2)_player.position + _followingOffset;
        }

        public void SetBGFollow(int stageIndex, bool isActive)
        {
            if(!isActive)   return;

            if(stageIndex < _fromStageIndex || stageIndex > _toStageIndex)
            {
                _isActive = false;
                return;
            }
            _isActive = true;
        }
    }
}