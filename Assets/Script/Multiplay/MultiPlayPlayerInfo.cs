using UnityEngine;
using NHD.UI.EmojiUI;

namespace NHD.Multiplay
{
    ///<summary>
    /// 멀티플레이시 플레이어 인스턴스의 정보를 가지는 클래스
    ///</summary>
    public class MultiPlayPlayerInfo : MonoBehaviour
    {
        public int _id;
        public string _username;
        public Transform _Player;
        public EmojiSpawner _emojiSpawner;

        private void Start()
        {
            _emojiSpawner = FindObjectOfType<EmojiSpawner>();
        }

        public void SetEmoji(int emojiIndex)
        {
            _emojiSpawner.SpawnEmoji(emojiIndex, this.transform);
        }
    }
}
