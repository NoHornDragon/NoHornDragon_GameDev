using NHD.Entity.Player;
using TMPro;
using UnityEngine;

namespace NHD.UI.InGameScene
{
    public class ResetUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject wiseSayingUI;
        [SerializeField]
        private TextMeshProUGUI wiseSayingText;
        [TextArea]
        private string[] resetMessage;


        private void Start()
        {
            FindObjectOfType<PlayerMovement>().PlayerResetEvent += ShowResetUI;
        }

        private void ShowResetUI(bool isActive)
        {
            if (!isActive) return;

            wiseSayingUI.SetActive(true);
        }
    }
}