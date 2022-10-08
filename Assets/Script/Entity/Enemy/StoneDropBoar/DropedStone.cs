using NHD.GamePlay.ObjectPool;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class DropedStone : PoolableObjectBase
    {
        private void OnEnable()
        {
            Invoke("DestroyStone", 10f);
        }

        public void DestroyStone()
        {
            InvokeReturnCall();
        }
    }
}
