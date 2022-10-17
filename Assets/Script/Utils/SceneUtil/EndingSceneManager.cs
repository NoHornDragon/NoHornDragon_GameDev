using UnityEngine;

namespace NHD.Utils.SceneUtil
{
    public class EndingSceneManager : MonoBehaviour
    {
        private void Start()
        {
            Invoke("BackToLobbyScene", 4f);
        }

        private void BackToLobbyScene()
        {
            SceneChangerSingleton._instance.ChangeSceneWithFadeOut("LobbyScene");
        }
    }
}