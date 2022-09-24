using NHD.GamePlay.ObjectPool;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class DropedStone : PoolableObjectBase
    {
        private void Start()
        {
            Invoke("DestroyStone", 10f);
        }

        public void DestroyStone()
        {
            InvokeReturnCall();
        }
    }
}
