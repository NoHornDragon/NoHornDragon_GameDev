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
        private Vector2 _newPos;
        private BackGroundScroller _bgScroller;
        
        private void Awake()
        {
            _initialPos = transform.localPosition;
            _bgScroller = FindObjectOfType<BackGroundScroller>();
        }

        private void OnEnable()
        {
            transform.localPosition = _initialPos;
            _bgScroller._playerMoveEvent += MoveLayer;
        }

        private void OnDisable()
        {
            _initialPos = transform.localPosition;
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
/*
현재 문제
- 새 스테이지 진입
- 카메라의 initial position이 달라짐
- 아직 기존 배경은 구독중임
- 구독으로 인해서 새 카메라의 initial position으로 계산이 되기 시작함
- 그래서 confiner를 한번에 움직이는게 아니면 힘들어 지는 것

해결법
- confiner를 인게임에서 떨어뜨려 놓는다
*/