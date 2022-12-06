using UnityEngine;

namespace NHD.Entity.Player
{
    public class AnotherMovement : MonoBehaviour
    {

        [SerializeField] private Vector3 _originPos;
        private bool _isActive;
        private Rigidbody2D _rigid;

        private void Start()
        {
            _originPos = this.transform.position;
            _rigid = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!_isActive)
                return;

            Movement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // TODO : 이거 태그 고려
            // if(other.CompareTag("Player"))
            // {
            //     BackToOrigin();
            // }
            if (other.CompareTag("Enemy"))
                BackToOrigin();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Room"))
            {
                BackToOrigin();
            }
        }

        public virtual void Movement() { }

        public void BackToOrigin()
        {
            // no input
            _isActive = false;

            gameObject.transform.position = _originPos;

            // TODO : player position to here

        }
    }
}