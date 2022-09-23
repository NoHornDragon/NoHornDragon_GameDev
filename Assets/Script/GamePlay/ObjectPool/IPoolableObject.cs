using UnityEngine;
using System;
namespace NHD.GamePlay.ObjectPool
{
    public abstract class IPoolableObject : MonoBehaviour
    {
        public event Action<IPoolableObject> _returnToPoolCallbackEvent;

        public void InvokeReturnCall()
        {
            _returnToPoolCallbackEvent?.Invoke(this);
        }
    }
}
