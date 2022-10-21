using NHD.UI.Common;
using NHD.Utils.SoundUtil;
using UnityEngine;

namespace NHD.UI.titleScene.creditPopup
{
    public class CreditPopup : MonoBehaviour, IPopup
    {
        [SerializeField] private AudioClip _closedSound;

        public void Setup()
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            SoundManager._instance.PlayEFXAmbient(_closedSound);
            this.gameObject.SetActive(false);
        }
    }
}