using NHD.GamePlay.Camera;
using UnityEngine;

namespace NHD.GamePlay.BackGroundEffect
{
    public class StageBackGroundManager : MonoBehaviour
    {
        [SerializeField] private uint _stageIndex;
        private bool _nowActive = true;
        [SerializeField] private Transform[] _bgImages;

        private void Start()
        {

            PlayerEnterConfiner[] confiners = FindObjectsOfType<PlayerEnterConfiner>();
            foreach (PlayerEnterConfiner confiner in confiners)
            {
                confiner.ActiveRoomEvent += StageEvent;
            }

            if (_stageIndex != 1)
                ActiveBG(false);
        }

        /// <summary>
        /// 활성화된 스테이지와 플레이어의 현재 스테이지가 맞지 않는 경우 배경 처리를 합니다.
        /// </summary>
        private void StageEvent(uint playerStage, bool isIn)
        {
            if (playerStage != _stageIndex) return;

            ActiveBG(isIn);
        }

        private void ActiveBG(bool isActive)
        {
            if (_nowActive == isActive) return;

            for (int i = 0; i < _bgImages.Length; i++)
            {
                _bgImages[i].gameObject.SetActive(isActive);
            }

            _nowActive = isActive;
        }
    }
}