using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterConfiner : MonoBehaviour
{
    PolygonCollider2D collider;
    [SerializeField] private float lensSize;
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<CameraManager>().ChangeCamera(collider, lensSize);
        }
    }
}
