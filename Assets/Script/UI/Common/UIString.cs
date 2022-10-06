using NHD.DataController.Loaders;
using NHD.StaticData.UIString;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.Common
{
    public class UIString : MonoBehaviour
    {
        [SerializeField] private string _stringKey;
        private Text _uiText;

        // Start is called before the first frame update
        private void Awake()
        {
            _uiText = GetComponent<Text>();
        }

        private void Start()
        {
            SetText();
        }

        private void OnEnable()
        {
            UIStringLoader.LanguageChangeAction += SetText;
        }

        private void OnDestroy()
        {
            UIStringLoader.LanguageChangeAction -= SetText;
        }

        public void SetText()
        {
            _uiText.text = StaticUIString._staticUIString[_stringKey];
        }
    }
}
