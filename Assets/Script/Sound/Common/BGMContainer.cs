using NHD.Utils.SoundUtil;

namespace NHD.Sound.Common
{
    public class BGMContainer : SoundPlayableBase
    {
        private void Start()
        {
            SoundManager._instance.PlayRandomBGM(_audioClips);
        }

        private void OnDisable()
        {
            SoundManager._instance.StopBGM();
        }
    }
}
