using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSavePoint : MonoBehaviour
{
    public Vector3 savePoint;

    [ContextMenu("Save this Point")]
    public void SaveThisPoint()
    {
        savePoint = transform.position;
    }

    [ContextMenu("Load Point")]
    public void LoadPoint()
    {
        transform.position = savePoint;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftCommand))
        {
            SaveThisPoint();
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            LoadPoint();
        }
    }
}
