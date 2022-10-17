using NHD.GamePlay.BackGroundEffect;
using System;
using UnityEngine;

namespace NHD.GamePlay.CameraComponent
{
    public class PlayerEnterConfiner : MonoBehaviour
    {
        public event Action<int, bool> ActiveRoomEvent;
        [SerializeField] private int _stageIndex;

        private void Start()
        {
            ActiveRoomEvent += FindObjectOfType<BackGroundScroller>().ChangeCameraPos;
            ActiveRoomEvent += FindObjectOfType<StageManager>().StageChange;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Debug.Log($"stage {_stageIndex} - Enter");
                ActiveRoomEvent?.Invoke(_stageIndex, true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Debug.Log($"stage {_stageIndex} - Exit");
                ActiveRoomEvent?.Invoke(_stageIndex, false);
            }
        }
    }
}