using DG.Tweening;
using NHD.StaticData.Settings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NHD.Utils.SoundUtil
{
	public class SoundManager : MonoBehaviour
    {
        public static SoundManager _instance;

        [Header("재생가능한 오디오 소스")]
        public AudioSource _audioSourceBGM;
        public AudioSource[] _audioSourcesEFX;

        [Header("게임에 사용되는 사운드")]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioClip[] _bgmSoundsData;
        [SerializeField] private AudioClip[] _efxSoundsData;
        private Dictionary<string, AudioClip> _bgmSounds = new Dictionary<string, AudioClip>();
        private Dictionary<string, AudioClip> _efxSounds = new Dictionary<string, AudioClip>();
        private List<string> _bgmNames = new List<string>();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            SetBGMVolume(StaticSettingsData._bgmVolume);
            SetEFXVolume(StaticSettingsData._effectVolume);
            SetDictionary();
        }

        private void SetDictionary()
		{
            foreach(var bgmSound in _bgmSoundsData)
			{
                _bgmSounds[bgmSound.name] = bgmSound;
                _bgmNames.Add(bgmSound.name);
			}
            foreach(var efxSound in _efxSoundsData)
			{
                _efxSounds[efxSound.name] = efxSound;
			}
		}

        public void SetBGMVolume(float volume)
		{
            _audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
		}

        public void SetEFXVolume(float volume)
		{
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
		}

        public void PlayBGM(string name, bool isFade = true)
        {
            _audioSourceBGM.clip = _bgmSounds[name];
            if (isFade) // Fade와 함께 bgm 실행
            {
                _audioSourceBGM.volume = 0.0f;
                _audioSourceBGM.Play();
                _audioSourceBGM.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
                return;
            }
            _audioSourceBGM.Play();
        }

        public void PlayRandomBGM()
        {
            int bgmIndex = Random.Range(0, _bgmNames.Count);
            _audioSourceBGM.clip = _bgmSounds[_bgmNames[bgmIndex]];
            _audioSourceBGM.DOFade(0.0f, 0.5f)
            .OnComplete(delegate () { 
                _audioSourceBGM.Play(); 
                _audioSourceBGM.DOFade(1.0f, 0.5f).SetEase(Ease.Linear); 
            }).SetEase(Ease.Linear);
        }

        /// <summary>
        /// 현재 실행중인 BGM 중지
        /// </summary>
        /// <param name="isFade"></param>
        public void StopBGM(bool isFade = true) // 즉시 bgm 중지
        {
            if (isFade)
            {
                _audioSourceBGM.DOFade(0.0f, 0.5f)
                .OnComplete(delegate () { 
                    _audioSourceBGM.volume = 1.0f; 
                    _audioSourceBGM.Stop(); 
                });
            }
            else
            {
                _audioSourceBGM.Stop();
            }
        }

        /// <summary>
        /// 효과음을 재생
        /// </summary>
        /// <param name="name"></param>
        public void PlayEFX(string name, Vector3 position)
        {
            foreach(var audioSourceEFX in _audioSourcesEFX)
			{
				if (!audioSourceEFX.isPlaying)
				{
                    audioSourceEFX.clip = _efxSounds[name];
                    AudioSource.PlayClipAtPoint(audioSourceEFX.clip, position);
				}
			}
        }
        /// <summary>
        /// 모든 효과음 중지
        /// </summary>
        public void StopAllEFX()
        {
            foreach (var audioSourceEFX in _audioSourcesEFX)
            {
                audioSourceEFX.Stop();
            }
        }
    }
}