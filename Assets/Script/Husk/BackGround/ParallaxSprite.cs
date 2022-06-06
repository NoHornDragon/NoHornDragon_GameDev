using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSprite : MonoBehaviour
{

    [Header("스크롤링 되는 정도")]
    [SerializeField] 
    [Tooltip("최대 0.5 까지 움직이는 정도를 설정할 수 있습니다. 0이면 움직임 X.")]
    [Range(0, 0.5f)]private float moveAmount;
    
    [Header("상하좌우 이동 잠그는 여부")]
    [Tooltip("체크하면 좌우 이동을 하지 않습니다.")]
    [SerializeField] private bool lockHorizontal;
    [Tooltip("체크하면 상하 이동을 하지 않습니다.")]
    [SerializeField] private bool lockVertical;
    bool canMove = true;
    [SerializeField] private SavePos initialPos;
    [SerializeField] Vector3 newPos;

    private void Awake()
    {
        initialPos = new SavePos(this.transform.position);
    }

    private void OnEnable()
    {
        FindObjectOfType<BackGroundScroller>().playerMoveEvent += MoveLayer;
    }
    
    private void OnDisable()
    {
        initialPos = new SavePos(this.transform.position);
        FindObjectOfType<BackGroundScroller>().playerMoveEvent -= MoveLayer;
    }

    void MoveLayer(float inputX, float inputY)
    {
        if (!canMove) return;

        if(!lockHorizontal)   newPos.x = initialPos.pos.x - (inputX * moveAmount);
        if(!lockVertical)     newPos.y = initialPos.pos.y - (inputY * moveAmount);

        transform.localPosition = newPos;
    }
}


/*
public class ParallaxSprite : MonoBehaviour
{

    [Header("스크롤링 되는 정도")]
    [SerializeField] 
    [Tooltip("최대 0.5 까지 움직이는 정도를 설정할 수 있습니다. 0이면 움직임 X.")]
    [Range(0, 0.5f)]private float moveAmount;
    
    [Header("상하좌우 이동 잠그는 여부")]
    [Tooltip("체크하면 좌우 이동을 하지 않습니다.")]
    [SerializeField] private bool lockHorizontal;
    [Tooltip("체크하면 상하 이동을 하지 않습니다.")]
    [SerializeField] private bool lockVertical;
    private Vector3 newPosition;

    [SerializeField] private Vector3 savePos;
    bool canMove = true;

    // 2. SavePositions
    bool prevSaved, forwardSaved;
    SavePos prevPos;
    SavePos forwardPos;
    // 1.
    SavePos InitPos;

    private void Awake()
    {
        savePos = transform.position;
        if (moveAmount == 0)
            Destroy(GetComponent<ParallaxSprite>());
        
        InitPos = new SavePos(GameObject.FindWithTag("Player").transform.position);
    }

    private void OnEnable()
    {
        FindObjectOfType<BackGroundScroller>().playerMoveEvent += MoveLayer;
        transform.position = savePos;

        StartCoroutine(Delay());
    }
    
    private void OnDisable()
    {
        FindObjectOfType<BackGroundScroller>().playerMoveEvent -= MoveLayer;

        savePos = transform.position;
        canMove = false;
    }

    IEnumerator Delay()
    {
        yield return null;

        canMove = true;
    }

    void MoveLayer(float x, float y)
    {
        if (!canMove) return;

        newPosition = transform.localPosition;
        if(!lockHorizontal)   newPosition.x -= x * moveAmount;
        if(!lockVertical)     newPosition.y -= y * moveAmount;

        transform.localPosition = newPosition;
    }
}


*/