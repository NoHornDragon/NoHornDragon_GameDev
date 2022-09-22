using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class DropedStone : MonoBehaviour
    {
        public StonesPool _stonesPool;

        private void Start()
        {
            Invoke("DestroyStone", 10f);
        }

        public void DestroyStone()
        {
            _stonesPool.ReturnObject(this);
        }
    }
}
