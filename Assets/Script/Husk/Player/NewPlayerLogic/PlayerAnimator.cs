using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private Animator anim;
    [SerializeField] private Transform baseObject;
    Rigidbody2D rigid;
    private Vector3 rightScale = new Vector3(1, 1, 1);
    private Vector3 leftScale = new Vector3(-1, 1, 1);
    private bool isright;
    void Start()
    {
        anim = GetComponent<Animator>();
        baseObject = transform.parent.transform;
        player = GetComponentInParent<PlayerMovement>();
        rigid = baseObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetBool("onGround", player.onGround);
        anim.SetBool("nowSwing", player.nowJoint);
        anim.SetBool("prepareThrow", player.prepareLaunch);
        anim.SetBool("throw", player.throwYeouiju);
        anim.SetBool("stuned", player.stuned);
        anim.SetBool("throwed", player.throwed);

        // if don't want flip while swing
        if(!player.nowJoint) 
        {
            // player flip by mouse direction
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                baseObject.localScale = rightScale;
            }
            else
            {
                baseObject.localScale = leftScale;
            }
            return;
        }

        // player flip by velocity
        if(player.onGround) return;
        if(rigid.velocity.x < 0)
        {
            baseObject.localScale = leftScale;
        }
        else
        {
            baseObject.localScale = rightScale;
        }

    }

    private void PlayerJointAnimation(Vector2 dummyinput)
    {
        this.transform.localPosition = new Vector3(-0.8f, -0.43f, 0);
    }

}
