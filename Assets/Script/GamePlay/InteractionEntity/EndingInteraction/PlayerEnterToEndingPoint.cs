using NHD.Utils.SceneUtil;
using UnityEngine;

namespace NHD.GamePlay.InteractionEntity.EndingInteraction 
{
    public class PlayerEnterToEndingPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                SceneChangerSingleton._instance.ChangeSceneWithFadeOut("EndingScene");
            }
        }
    }
}