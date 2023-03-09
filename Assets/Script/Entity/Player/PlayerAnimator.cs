using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace NHD.Entity.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private Animator _anim;
        [SerializeField] private SpriteResolver _stunSpriteResolver;
        private readonly string STUNED_HEAD = "stun head";
        private readonly string NORMAL_HEAD = "head";
        [SerializeField] private Rigidbody2D _rigid;
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Vector3 _leftScale = new Vector3(-1, 1, 1);

        void Update()
        {
            _anim.SetBool("onGround", _player._onGround);
            _anim.SetBool("nowSwing", _player._nowJoint);
            _anim.SetBool("prepareThrow", _player._prepareLaunch);
            _anim.SetBool("throw", _player._throwYeouiju);
            _anim.SetBool("stuned", _player._stuned);
            _anim.SetBool("throwed", _player._throwed);

            _stunSpriteResolver.SetCategoryAndLabel("Head", (_player._stuned ? STUNED_HEAD : NORMAL_HEAD));

            // player flip by mouse direction
            if (!_player._nowJoint)
            {
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                {
                    transform.localScale = _rightScale;
                }
                else
                {
                    transform.localScale = _leftScale;
                }
                return;
            }

            // player flip by velocity when jointed
            if (_player._onGround) return;
            if (_rigid.velocity.x < 0)
            {
                transform.localScale = _leftScale;
            }
            else
            {
                transform.localScale = _rightScale;
            }
        }
    }
}