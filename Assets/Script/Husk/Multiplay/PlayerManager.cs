﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public Transform Player;

    [SerializeField]
    private TextMeshPro userNameText;

    private void Start()
    {
        userNameText.text = username;
    }
}
