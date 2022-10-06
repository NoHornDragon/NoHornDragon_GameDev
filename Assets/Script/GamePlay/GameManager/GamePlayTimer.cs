using NHD.GamePlay.BackGroundEffect;
using NHD.UI.inGameScene;
using UnityEngine;

namespace NHD.GamePlay.GameManager
{
    public class GamePlayTimer : MonoBehaviour
    {
        [SerializeField]
        private float _playTime = 0.0f;
        private bool _timerActive = true;
        [SerializeField]
        private Material _seasonMaterial;

        private void Start()
        {
            FindObjectOfType<InGameScene>().TimerSetEvent += SetTimerStatue;

            _seasonMaterial = FindObjectOfType<ParallaxSprite>().GetComponent<SpriteRenderer>().sharedMaterial;
        }

        public void SetTimerStatue(bool isActive)
        {
            _timerActive = isActive;
        }

        public float GetPlayTime()
        {
            return _playTime;
        }

        private void Update()
        {
            if (!_timerActive) return;

            _playTime += Time.deltaTime;

            // float materialValue = Mathf.Sin(_playTime * 0.003f);
            // materialValue = Mathf.Abs(materialValue);
            _seasonMaterial.SetFloat("_SeasonValue", _playTime);
        }

    }
}