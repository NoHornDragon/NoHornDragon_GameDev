using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HistoryNode : MonoBehaviour
{
    public Sprite image;

    [Header("종이 이름, text[0] = kor, text[1] = eng")]
    public string[] title;
    
    [Header("종이 설명, text[0] = kor, text[1] = eng")]
    public string[] description;
}
