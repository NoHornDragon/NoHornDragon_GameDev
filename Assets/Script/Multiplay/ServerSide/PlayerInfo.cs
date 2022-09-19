using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class PlayerInfo : MonoBehaviour
    {
        public int _id;
        public string _username;
        public Vector3 _position;
        public Quaternion _rotation;

        public int _emojiIndex = -1;

        public void Initialize(int id, string username)
        {
            _id = id;
            _username = username;
            _emojiIndex = -1;
        }

        public void Update()
        {
            Move(_position);

            if(_emojiIndex != -1)
            {
                ServerSend.PlayerEmojied(this);
                _emojiIndex = -1;
            }
        }

        private void Move(Vector3 nextPosition)
        {
            ServerSend.PlayerPosition(this);
        }

        public void SetPosition(Vector3 position, Quaternion rotation)
        {
            _position = position;
            _rotation = rotation;
        }

        public void TriggerEmoji(int emojiIndex)
        {
            Debug.Log($"[{emojiIndex}] : Trigger Emoji");
            _emojiIndex = emojiIndex;
        }
    }
}