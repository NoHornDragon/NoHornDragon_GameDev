using NHD.UI.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.inGameScene
{
    public class InGameScene : MonoBehaviour
    {
		[SerializeField] private GameObject _pausePopup;
		public bool _isSceneLoading;

		private void OnEnable()
		{
			_isSceneLoading = false;
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
					OpenPausePopup();
				}
				else
				{
					PopupContainer.PopPopup();
				}
			}
		}

		public void OpenPausePopup()
		{
			PopupContainer.PushPopup(_pausePopup.GetComponent<IPopup>());
		}
	}
}
