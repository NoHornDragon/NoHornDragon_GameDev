using UnityEngine;
using NHD.UI.EmojiUI;

namespace NHD.Multiplay
{
    public class PlayerManager : MonoBehaviour
    {
        public int id;
        public string username;
        public Transform Player;

        public void SetEmoji(int emojiIndex)
        {
            FindObjectOfType<EmojiSpawner>().SpawnEmoji(emojiIndex, this.transform);
        }
    }
}
