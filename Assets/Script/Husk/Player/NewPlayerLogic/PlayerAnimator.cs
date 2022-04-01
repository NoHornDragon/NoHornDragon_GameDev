using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement player;
    private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    private bool isright;
    void Start()
    {
        // anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerMovement>();

        FindObjectOfType<PlayerCollider>().playerChangeEvent += SetPlayerSprite;
        FindObjectOfType<YeouijuReflection>().collisionEvent += PlayerJointAnimation;
        FindObjectOfType<YeouijuLaunch>().disJointEvent += PlayerDisjointAnimation;
    }

    void Update()
    {
        // anim.SetBool("onGround", player.onGround);
        // anim.SetBool("nowSwing", player.nowJoint);
        // anim.SetBool("prepareThrow", player.prepareLaunch);
        // anim.SetBool("throw", player.throwYeouiju);
        // anim.SetBool("stuned", player.stuned);

    }

    public void SetPlayerSprite(bool isActive)
    {
        sprite.enabled = isActive;
    }

    private void PlayerJointAnimation(Vector2 dummyinput)
    {
        this.transform.localPosition = new Vector3(-0.8f, -0.43f, 0);
    }

    private void PlayerDisjointAnimation()
    {
        this.transform.localPosition = Vector3.zero;
    }
}
