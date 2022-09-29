using UnityEngine;

namespace NHD.Entity.NPC.TalkingNPC
{
    public class NPCFarCheck : MonoBehaviour
    {
        public NPC currentNPC;

        private void Awake()
        {
            currentNPC = GetComponentInParent<NPC>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                currentNPC.visitCount++;
            }
        }
    }
}