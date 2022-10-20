using NHD.UI.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.titleScene.creditPopup
{
    public class CreditPopup : MonoBehaviour, IPopup
    {
        public void Setup()
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            this.gameObject.SetActive(false);
        }
    }
}