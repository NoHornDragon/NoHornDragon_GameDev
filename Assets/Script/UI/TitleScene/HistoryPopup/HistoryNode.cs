using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.TitleScene.HistoryPopup
{
    public class HistoryNode : MonoBehaviour
    {
        public Sprite image;

        [Header("종이 이름, text[0] = kor, text[1] = eng")]
        [TextArea]
        public string[] title;

        [Header("종이 설명, text[0] = kor, text[1] = eng")]
        [TextArea]
        public string[] description;

        public Image childImage;

        private void Awake()
        {
            childImage = transform.GetChild(0).GetComponent<Image>();
        }
    }
}