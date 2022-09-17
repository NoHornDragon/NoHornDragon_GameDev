using UnityEngine;

namespace NHD.UI.EmojiUI
{
    public class EmojiInGame : MonoBehaviour
    {
        private void Start()
        {
            Destroy(this.gameObject, 12.5f);
        }
    }
}