using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Sound.Common
{
    public abstract class SoundPlayableBase : MonoBehaviour
    {
        [SerializeField]
        protected List<AudioClip> _audioClips = new List<AudioClip>();
    }
}