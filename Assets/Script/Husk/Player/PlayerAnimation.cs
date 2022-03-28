using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    private Player player;
    
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
        // anim = GetComponent<Animator>();

        
    }

    private void Update()
    {
        // anim.SetBool("onGround", player.onGround);
        // anim.SetBool("nowSwing", player.nowSwing);
        // anim.SetBool("prepareThrow", player.prepareLaunch);
        // anim.SetBool("throw", player.throwYeouiju);
        // anim.SetBool("stuned", player.stuned);
    }

    public void FlipX(bool isRight)
    {
        sprite.flipX = isRight;
    }


}
