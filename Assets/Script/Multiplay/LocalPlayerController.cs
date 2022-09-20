using NHD.Multiplay.ClientSide;
using UnityEngine;

namespace NHD.Multiplay
{
    public class LocalPlayerController : MonoBehaviour
    {
        [SerializeField] private Transform Player;
        private void FixedUpdate()
        {
            SendPositionToServer();
        }

        private void SendPositionToServer()
        {
            Vector3 position = Player.position;

            // Debug.Log($"send local position {position}");
            ClientSend.PlayerMovement(position);
        }

        public void SendEmojiInfoToServer(int emojiIndex)
        {
            // Debug.Log($"[{emojiIndex}] : Send Emoji from local player");
            ClientSend.PlayerSendEmoji(emojiIndex);
        }
    }
}