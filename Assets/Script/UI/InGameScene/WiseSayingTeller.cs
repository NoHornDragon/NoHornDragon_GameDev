using NHD.Entity.Player;
using NHD.StaticData.Settings;
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
        [SerializeField]
        private WiaeSayingData wiseSayingTexts;

        [SerializeField] private GameObject wiseSayingUI;
        [SerializeField] private TextMeshProUGUI wiseSayingText;

        [Header("명언 파일 이름")]
        [SerializeField]
        private string wiseSayingFileName;



        private void Awake()
        {
            ReadWiseFile();
        }

        void Start()
        {
            wiseSayingUI.SetActive(false);
            FindObjectOfType<PlayerCollider>().playerStunEvent += ShowWiseSayingOnScreen;
        }


        [ContextMenu("명언 텍스트 로드")]
        private void ReadWiseFile()
        {
            if (!File.Exists(Application.dataPath + "/Resources/WiseSayingTexts/" + wiseSayingFileName + ".json"))
            {
                Debug.Log(wiseSayingFileName + " load failed");
            }
            else
            {
                FileStream fs = new FileStream(string.Format("{0}/Resources/WiseSayingTexts/{1}.json", Application.dataPath, wiseSayingFileName), FileMode.Open);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                fs.Close();

                string jsonData = Encoding.UTF8.GetString(data);
                wiseSayingTexts = JsonUtility.FromJson<WiaeSayingData>(jsonData);
            }
        }

        [ContextMenu("명언 UI ON")]
        private void ShowWiseSayingOnScreen(bool isActive)
        {
            // Have a appear tweening wiseSayingUI's OnEnable()
            // And will disappear automatically
            if (!isActive) return;

            wiseSayingText.text = wiseSayingTexts.texts[Random.Range(0, wiseSayingTexts.texts.Count)].wiseSay[StaticSettingsData._languageIndex];
            wiseSayingUI.SetActive(true);

        }
    }
}