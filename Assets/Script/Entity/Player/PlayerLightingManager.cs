using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using NHD.GamePlay.CameraComponent;

namespace NHD.Entity.Player
{
    public class PlayerLightingManager : MonoBehaviour
    {
        [SerializeField] private Light2D _playerLight;

        private void Start()
        {
            _playerLight = GetComponent<Light2D>();

            PlayerEnterConfiner[] confiners = FindObjectsOfType<PlayerEnterConfiner>();
            foreach (PlayerEnterConfiner confiner in confiners)
            {
                confiner.ActiveRoomEvent += SetBGFollow;
            }
        }

        public void SetBGFollow(int stageIndex, bool isActive)
        {
            if(!isActive)   return;
            
            switch(stageIndex)
            {
                case 1 :
                    _playerLight.intensity = 0.5f;
                    break;
                default :
                    _playerLight.intensity = 0.2f;
                    break;
            }
        }
    }
}