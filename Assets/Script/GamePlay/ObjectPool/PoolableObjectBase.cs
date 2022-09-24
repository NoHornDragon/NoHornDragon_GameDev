using UnityEngine;
using System;
namespace NHD.GamePlay.ObjectPool
{
    public abstract class PoolableObjectBase : MonoBehaviour
    {
        public event Action<PoolableObjectBase> ReturnToPoolCallbackEvent;

        public void InvokeReturnCall()
        {
            ReturnToPoolCallbackEvent?.Invoke(this);
        }
    }
}
