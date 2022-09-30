using NHD.GamePlay.BackGroundEffect;
using System;
using UnityEngine;

namespace NHD.GamePlay.Camera
{
    public class PlayerEnterConfiner : MonoBehaviour
    {
        public event Action<uint, bool> _activeRoomEvent;
        [SerializeField] private uint _stageIndex;

        private void Start()
        {
            _activeRoomEvent += FindObjectOfType<BackGroundScroller>().ChangeCameraPos;
            _activeRoomEvent += FindObjectOfType<StageManager>().StageChange;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Debug.Log($"stage {stageIndex} - Enter");
                _activeRoomEvent?.Invoke(_stageIndex, true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Debug.Log($"stage {stageIndex} - Exit");
                _activeRoomEvent?.Invoke(_stageIndex, false);
            }
        }
    }
}