using NHD.GamePlay.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class StonesPool : MonoBehaviour, IObjectPool
    {
        [SerializeField] private GameObject _stonePrefab;
        public Queue<PoolableObjectBase> _stonesPoolQueue = new Queue<PoolableObjectBase>();

        private void Awake()
        {
            Initialize(5);
        }

        private void Initialize(int initCount)
        {
            for(int i = 0; i < initCount; i++)
            {
                SupplyObjectPool();
            }
        }

        public PoolableObjectBase GetObjectFromPool()
        {
            var obj = _stonesPoolQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void InsertObjectToPool(PoolableObjectBase obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            _stonesPoolQueue.Enqueue(obj);
        }

        public void SupplyObjectPool()
        {
            var newObject = Instantiate(_stonePrefab).GetComponent<PoolableObjectBase>();
            newObject.ReturnToPoolCallbackEvent += InsertObjectToPool;

            InsertObjectToPool(newObject);
        }
    }
}
