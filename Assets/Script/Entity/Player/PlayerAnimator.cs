using UnityEngine;

namespace NHD.Entity.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;
        [SerializeField] private Animator anim;
        [SerializeField] private Rigidbody2D rigid;
        private Vector3 rightScale = new Vector3(1, 1, 1);
        private Vector3 leftScale = new Vector3(-1, 1, 1);
        private bool isright;
        void Awake()
        {
            // anim = GetComponent<Animator>();
            // player = GetComponentInParent<PlayerMovement>();
            // rigid = transform.parent.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            anim.SetBool("onGround", player.onGround);
            anim.SetBool("nowSwing", player.nowJoint);
            anim.SetBool("prepareThrow", player.prepareLaunch);
            anim.SetBool("throw", player.throwYeouiju);
            anim.SetBool("stuned", player.stuned);
            anim.SetBool("throwed", player.throwed);

            // player flip by mouse direction
            if (!player.nowJoint)
            {
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                {
                    transform.localScale = rightScale;
                }
                else
                {
                    transform.localScale = leftScale;
                }
                return;
            }

            // player flip by velocity when jointed
            if (player.onGround) return;
            if (rigid.velocity.x < 0)
            {
                transform.localScale = leftScale;
            }
            else
            {
                transform.localScale = rightScale;
            }
        }
    }
}