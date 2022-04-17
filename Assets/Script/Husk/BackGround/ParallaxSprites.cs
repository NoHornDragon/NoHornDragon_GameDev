using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSprites : MonoBehaviour
{
    [Header("스크롤링 되는 정도")]
    [SerializeField] 
    [Range(0, 1)]private float moveAmount;
    
    [Header("상하좌우 이동 잠그는 여부")]
    [SerializeField] private bool lockHorizontal;
    [SerializeField] private bool lockVertical;
    Vector3 newPosition;

    void Start()
    {
        FindObjectOfType<BackGroundScroller>().playerMoveEvent += MoveLayer;
    }

    void MoveLayer(float x, float y)
    {
        newPosition = transform.localPosition;
        if(!lockHorizontal)   newPosition.x -= x * moveAmount;
        if(!lockVertical)     newPosition.y -= y * moveAmount;

        transform.localPosition = newPosition;
    }
}
