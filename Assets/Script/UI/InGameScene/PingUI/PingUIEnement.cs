using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingUI
{
    public class PingUIEnement : MonoBehaviour
    {
        [SerializeField] private int pingIconIndex;
        private PingUIManager pingUIManager;

        private void Start()
        {
            pingUIManager = transform.parent.parent.GetComponent<PingUIManager>();
        }

        public void SetPingIndex()
        {
            pingUIManager.PingIndex = pingIconIndex;
        }
    }
}

