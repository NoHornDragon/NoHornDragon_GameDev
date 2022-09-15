using NHD.Entity.NPC.AttackingNPC;
using UnityEngine;

namespace NHD.GamePlay.InteractionEntity.FiringObject
{
    public class FiringObject : MonoBehaviour
    {
        private Rigidbody2D rigid;

        protected FirePool firePool;
        public FirePool SetFirePool { set { firePool = value; } }


        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public virtual void Stop()
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
