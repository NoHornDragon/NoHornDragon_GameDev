using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement player;
    private Animator anim;
    [SerializeField] private Transform baseObject;
    private Vector3 rightScale = new Vector3(1, 1, 1);
    private Vector3 leftScale = new Vector3(-1, 1, 1);
    [SerializeField] private SpriteRenderer sprite;
    private bool isright;
    void Start()
    {
        // anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerMovement>();
        baseObject = transform.parent.parent;

        FindObjectOfType<PlayerCollider>().playerChangeEvent += SetPlayerSprite;
    }

    void Update()
    {
        // anim.SetBool("onGround", player.onGround);
        // anim.SetBool("nowSwing", player.nowJoint);
        // anim.SetBool("prepareThrow", player.prepareLaunch);
        // anim.SetBool("throw", player.throwYeouiju);
        // anim.SetBool("stuned", player.stuned);

        if(player.nowJoint) return;
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            baseObject.localScale = rightScale;
        }
        else
        {
            baseObject.localScale = leftScale;
        }

    }

    public void SetPlayerSprite(bool isActive)
    {
        sprite.enabled = isActive;
    }

    private void PlayerJointAnimation(Vector2 dummyinput)
    {
        this.transform.localPosition = new Vector3(-0.8f, -0.43f, 0);
    }

}
