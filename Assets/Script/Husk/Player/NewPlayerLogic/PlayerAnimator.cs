using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement player;
    private Animator anim;
    private SpriteRenderer sr;
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        anim.SetBool("onGround", player.onGround);
        anim.SetBool("nowSwing", player.nowJoint);
        anim.SetBool("prepareThrow", player.prepareLaunch);
        anim.SetBool("throw", player.throwYeouiju);
        anim.SetBool("stuned", player.stuned);

        sr.flipX = player.PlayerFlip();
    }
}
