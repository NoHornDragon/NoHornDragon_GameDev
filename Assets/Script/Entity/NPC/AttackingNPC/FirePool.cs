using System.Collections.Generic;
using UnityEngine;
using NHD.GamePlay.ObjectPool;

namespace NHD.Entity.NPC.AttackingNPC
{
    public class FirePool : MonoBehaviour, IObjectPool
    {
        [SerializeField] private PoolableObjectBase _fireObjectPrefab;
        private Queue<PoolableObjectBase> _fireItemPool = new Queue<PoolableObjectBase>();

        private void Start()
        {
            SupplyObjectPool();
            SupplyObjectPool();
        }

        public PoolableObjectBase GetObjectFromPool()
        {
            if (_fireItemPool.Count <= 0)
            {
                SupplyObjectPool();
            }

            var item = _fireItemPool.Dequeue();
            item.transform.localPosition = Vector3.zero;
            item.transform.SetParent(null);
            item.gameObject.SetActive(true);

            return item;
        }

        public void ReturnObjectToPool(PoolableObjectBase returnObj)
        {
            returnObj.gameObject.SetActive(false);
            returnObj.transform.SetParent(this.transform);
            returnObj.transform.localPosition = Vector3.zero;

            _fireItemPool.Enqueue(returnObj);
        }

        public void SupplyObjectPool()
        {
            var item = GameObject.Instantiate(_fireObjectPrefab).GetComponent<PoolableObjectBase>();
            item._returnToPoolCallbackEvent += ReturnObjectToPool;

            ReturnObjectToPool(item);
        }
    }
}