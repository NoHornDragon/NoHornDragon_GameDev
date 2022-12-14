using NHD.Entity.Player;
using TMPro;
using UnityEngine;

namespace NHD.UI.InGameScene
{
    public class ResetUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _resetUI;


        private void Start()
        {
            FindObjectOfType<PlayerMovement>().PlayerPositionResetEvent += ShowResetUI;
        }

        private void ShowResetUI(bool isActive)
        {
            if (!isActive) return;

            _resetUI.SetActive(true);
        }
    }
}