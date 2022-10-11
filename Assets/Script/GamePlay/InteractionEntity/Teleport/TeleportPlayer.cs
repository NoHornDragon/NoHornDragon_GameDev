using UnityEngine;
using System.Collections;
using NHD.Utils.SceneUtil;

namespace NHD.GamePlay.InteractionEntity.Teleport
{
    public class TeleportPlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform _outPosition;
        [SerializeField]
        private Vector2 _outForce;
        private WaitForSeconds _delayTime = new WaitForSeconds(1f);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                StartCoroutine(TeleportCourtine(other.gameObject));
            }
        }

        IEnumerator TeleportCourtine(GameObject player)
        {
            SceneChangerSingleton._instance.FadeOutAndIn();
            yield return _delayTime;

            player.transform.position = _outPosition.position;

            yield return null;

            var playerRigid = player.GetComponent<Rigidbody2D>();
            playerRigid.AddForce(_outForce);
        }
    }
}
