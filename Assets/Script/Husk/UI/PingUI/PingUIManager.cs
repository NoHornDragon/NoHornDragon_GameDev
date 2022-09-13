using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingUI
{
    public class PingUIManager : MonoBehaviour
    {
        private bool mouseButtonDown;
        [SerializeField] private int pingIndexToPopup = -1;
        public int PingIndex { set{ pingIndexToPopup = value; } }
        // should target to PingUI in inspector
        [SerializeField] private GameObject pingInterface;
        // Should target to player in inspector
        [SerializeField] private Transform whereToPopup;

        private void Update()
        {
            if(Input.GetMouseButtonDown(2))
                SetPingUI(true);
            if(Input.GetMouseButtonUp(2))
                SetPingUI(false);                
        }
        
        /// <summary>
        /// Get ping index from ui element
        /// </summary>
        /// <param name="index">Ping icon index</param>
        public void GetPingIndex()
        {
            if(pingIndexToPopup == -1)  return;

            // TODO : 여기서 인덱스를 받아 whereToTaret 에 처리
            Debug.Log($"{pingIndexToPopup}");
            pingIndexToPopup = -1;
        }


        private void SetPingUI(bool isActive)
        {
            pingInterface.SetActive(isActive);

            if(!isActive)
                GetPingIndex();
        }
    }
}