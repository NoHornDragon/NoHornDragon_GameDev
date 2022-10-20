using NHD.UI.Common;
using NHD.Utils.SoundUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.inGameScene
{
    public class InGameScene : MonoBehaviour
    {
		[SerializeField] private GameObject _pausePopup;
		public bool _isSceneLoading;
		public event Action<bool> TimerSetEvent;

		private void OnEnable()
		{
			_isSceneLoading = false;
		}

        private void Start()
        {
			SoundManager._instance.PlayRandomBGM();
        }

        private void Update()
		{
			if (!_isSceneLoading)
			{
				CheckInputKey();
			}
		}

		private void CheckInputKey()
		{
			OnInputEscKey();
		}

		private void OnInputEscKey()
		{
			if (Input.GetKeyUp(KeyCode.Escape))
			{
				if(PopupContainer._popupContainer.Count == 0)
				{
					TimerSetEvent?.Invoke(false);
					OpenPausePopup();
				}
				else
				{
					PopupContainer.PopPopup();
					if(PopupContainer._popupContainer.Count == 0)
                    {
						TimerSetEvent?.Invoke(true);
					}
				}
			}
		}

		public void OpenPausePopup()
		{
			PopupContainer.PushPopup(_pausePopup.GetComponent<IPopup>());
		}
	}
}
