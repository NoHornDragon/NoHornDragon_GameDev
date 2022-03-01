using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Player player;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
        // anim = GetComponent<Animator>();
    }
    /*
    플레이어 애니메이션
    - 던지는 모션
    - 던지기 모션
    - 줄타는 모션
    */

    private void Update()
    {
        // anim.SetBool("onGround", player.onGround);
        // anim.SetBool("nowSwing", player.nowSwing);
        // anim.SetBool("prepareThrow", player.prepareLaunch);
        // anim.SetBool("throw", player.throwYeouiju);
    }

    public void FlipX(bool isRight)
    {
        spriteRenderer.flipX = isRight;
    }
}
