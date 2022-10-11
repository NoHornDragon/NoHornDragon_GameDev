using NHD.StaticData.NPCComment;
using DG.Tweening;
using NHD.StaticData.Settings;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

namespace NHD.Entity.NPC.TalkingNPC
{
    [System.Serializable]
    public class NPCTalks
    {
        public int _count;
        public string _key;
    }

    public class NPC : MonoBehaviour
    {
        private Coroutine _crtPtr;
        private StringBuilder _textBuilder = new StringBuilder("");
        private BoxCollider2D _npcCol;
        [Header("플레이어 인식 거리")]
        public float _catchDistance;
        [Header("플레이어 멀리갔음을 인식하는 거리")]
        public float _farDistance;
        [Header("그 외")]
        [SerializeField] private GameObject _visitText; // NPC를 방문하면 출력되는 텍스트
        [SerializeField] private GameObject _textBox; // 텍스트 박스
        [SerializeField] private NPCTalks[] _npcTalks;
        public int _visitCount; // NPC를 방문한 횟수
        public BoxCollider2D _farCheckCol; // NPC에게서 멀어짐을 체크하는 함수
        
        private void Awake()
        {
            _npcCol = this.GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            SetColSize();
            SetFarColSize();
        }

        private void SetColSize()
        {
            _npcCol.size = new Vector2(_catchDistance, _catchDistance);
        }

        private void SetFarColSize()
        {
            _farCheckCol.size = new Vector2(_farDistance, _farDistance);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _textBox.SetActive(true);
                _visitText.SetActive(true);
                _crtPtr = StartCoroutine(TalkCoroutine());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (_crtPtr != null)
                    StopCoroutine(_crtPtr);
                _visitText.SetActive(false);
                _textBox.SetActive(false);
            }
        }

        IEnumerator TalkCoroutine()
        {
            _textBuilder.Clear();
            int npcTalksLength = _npcTalks.Length;
            for (int i = npcTalksLength - 1; i >= 0; --i)
            {
                if (_visitCount >= _npcTalks[i]._count)
                {
                    int npcTextLength = StaticNPCCommentsData._staticNPCCommentsData[_npcTalks[i]._key].Length;
                    for (int j = 0; j < npcTextLength; ++j)
					{
                        _textBuilder.Append(StaticNPCCommentsData._staticNPCCommentsData[_npcTalks[i]._key][j]);
                        _visitText.GetComponent<TextMeshPro>().text = _textBuilder.ToString();
                        yield return new WaitForSeconds(0.05f);
                    }
                    _visitText.GetComponent<TextMeshPro>().text = StaticNPCCommentsData._staticNPCCommentsData[_npcTalks[i]._key];
                    break;
                }
            }
        }
    }
}