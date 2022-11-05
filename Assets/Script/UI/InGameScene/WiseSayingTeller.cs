using NHD.Entity.Player;
using NHD.StaticData.Settings;
using NHD.StaticData.WiseSaying;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

namespace NHD.UI.InGameScene
{
    [System.Serializable]
    public class WiseSaying
    {
        [Header("talk[0] = kor, talk[1] = eng")]
        public string[] wiseSay;
    }

    [System.Serializable]
    public class WiaeSayingData
    {
        public List<WiseSaying> texts;
    }


    public class WiseSayingTeller : MonoBehaviour
    {
        [SerializeField] private GameObject wiseSayingUI;
        [SerializeField] private TextMeshProUGUI wiseSayingText;

        private string _keyValue = "WISESAYING_";

        void Start()
        {
            wiseSayingUI.SetActive(false);
            FindObjectOfType<PlayerCollider>().playerStunEvent += ShowWiseSayingOnScreen;
        }

        [ContextMenu("명언 UI ON")]
        private void ShowWiseSayingOnScreen(bool isActive)
        {
            // Have a appear tweening wiseSayingUI's OnEnable()
            // And will disappear automatically
            if (!isActive) return;
            string key = $"{_keyValue}{Random.Range(1, StaticWiseSayingData._staticWiseSayingData.Count + 1)}";
            wiseSayingText.text = StaticWiseSayingData._staticWiseSayingData[key];
            wiseSayingUI.SetActive(true);
        }
    }
}