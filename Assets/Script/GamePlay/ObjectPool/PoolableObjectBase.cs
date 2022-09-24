using UnityEngine;
using System;
namespace NHD.GamePlay.ObjectPool
{
    public abstract class PoolableObjectBase : MonoBehaviour
    {
        public event Action<PoolableObjectBase> _returnToPoolCallbackEvent;

        public void InvokeReturnCall()
        {
            _returnToPoolCallbackEvent?.Invoke(this);
        }
    }
}
