using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement player;
    private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    void Start()
    {
        // anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerMovement>();

        FindObjectOfType<PlayerCollider>().playerChangeEvent += SetPlayerSprite;
    }

    void Update()
    {
        // anim.SetBool("onGround", player.onGround);
        // anim.SetBool("nowSwing", player.nowJoint);
        // anim.SetBool("prepareThrow", player.prepareLaunch);
        // anim.SetBool("throw", player.throwYeouiju);
        // anim.SetBool("stuned", player.stuned);

        sprite.flipX = player.PlayerFlip();
    }

    public void SetPlayerSprite(bool isActive)
    {
        sprite.enabled = isActive;
    }
}
