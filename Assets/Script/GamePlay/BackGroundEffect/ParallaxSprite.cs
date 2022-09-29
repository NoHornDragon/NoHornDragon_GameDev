using UnityEngine;

namespace NHD.GamePlay.BackGroundEffect
{
    public class ParallaxSprite : MonoBehaviour
    {

        [Header("스크롤링 되는 정도")]
        [SerializeField]
        [Tooltip("최대 0.5 까지 움직이는 정도를 설정할 수 있습니다. 0이면 움직임 X.")]
        [Range(0, 0.5f)] private float _moveAmount;

        [Header("상하좌우 이동 잠그는 여부")]
        [Tooltip("체크하면 좌우 이동을 하지 않습니다.")]
        [SerializeField] private bool _lockHorizontal;
        [Tooltip("체크하면 상하 이동을 하지 않습니다.")]
        [SerializeField] private bool _lockVertical;
        private Vector2 _initialPos;
        private Vector3 _newPos;
        private BackGroundScroller _bgScroller;

        private void Awake()
        {
            _initialPos = this.transform.position;
            _bgScroller = FindObjectOfType<BackGroundScroller>();
            
        }

        private void OnEnable()
        {
            _bgScroller._playerMoveEvent += MoveLayer;
        }

        private void OnDisable()
        {
            _initialPos = this.transform.position;
            _bgScroller._playerMoveEvent -= MoveLayer;
        }

        void MoveLayer(float inputX, float inputY)
        {
            if (!_lockHorizontal)   _newPos.x = _initialPos.x - (inputX * _moveAmount);
            if (!_lockVertical)     _newPos.y = _initialPos.y - (inputY * _moveAmount);

            transform.localPosition = _newPos;
        }
    }
}