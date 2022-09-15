using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class Player : MonoBehaviour
    {
        public int id;
        public string username;
        public Vector3 position;
        public Quaternion rotation;

        public void Initialize(int _id, string _username)
        {
            id = _id;
            username = _username;
        }

        public void Update()
        {
            Move(position);
        }

        private void Move(Vector3 _nextPosition)
        {
            ServerSend.PlayerPosition(this);
        }

        public void SetPosition(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }
    }
}