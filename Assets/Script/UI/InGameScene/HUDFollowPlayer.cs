using NHD.StaticData.Settings;
using UnityEngine;

namespace NHD.UI.InGameScene
{
    public class HUDFollowPlayer : MonoBehaviour
    {
        private Transform playerPos;
        [SerializeField] private Vector3 UIOffset;
        private Camera cam;
        void Start()
        {
            // if using easy mode, game don't need this hud
            if(!StaticSettingsData._isHardMode)    Destroy(gameObject);
            

            playerPos = transform.parent.parent;
            cam = Camera.main;
        }

        void Update()
        {
            // following player
            transform.position = cam.WorldToScreenPoint(playerPos.position + UIOffset);
        }
    }
}