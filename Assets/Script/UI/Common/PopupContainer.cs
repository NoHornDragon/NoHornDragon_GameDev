using System.Collections.Generic;

namespace NHD.UI.Common
{
    public class PopupContainer
    {
        public static Stack<IPopup> _popupContainer = new Stack<IPopup>();
        public static int _popupCount = 0;

        public static void PushPopup(IPopup popup)
        {
            _popupContainer.Push(popup);
            ++_popupCount;
            popup.Setup();
        }

        public static void PopPopup()
        {
            _popupContainer.Pop();
            --_popupCount;
        }
    }
}
