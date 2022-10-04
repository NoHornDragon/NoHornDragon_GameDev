using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.Common
{
    public class PopupContainer
    {
        public static Stack<IPopup> _popupContainer = new Stack<IPopup>();

        public static void PushPopup(IPopup popup)
        {
            _popupContainer.Push(popup);
            popup.Setup();
        }

        public static void PopPopup()
        {
            _popupContainer.Pop().ClosePopup();
        }

        public static void ClearStack()
        {
            while(!(_popupContainer.Count == 0))
            {
                _popupContainer.Pop();
            }
        }
    }
}
