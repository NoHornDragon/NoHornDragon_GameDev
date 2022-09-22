using System.Collections.Generic;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class StonesPool : MonoBehaviour
    {
        [SerializeField] private GameObject _stonePrefab;
        public Queue<DropedStone> _stonesPoolQueue = new Queue<DropedStone>();

        private void Awake()
        {
            Initialize(5);
        }

        private void Initialize(int initCount)
        {
            for(int i = 0; i < initCount; i++)
            {
                _stonesPoolQueue.Enqueue(CreateNewObject());
            }
        }

        private DropedStone CreateNewObject()
        {
            var newObject = Instantiate(_stonePrefab).GetComponent<DropedStone>();
            newObject.gameObject.SetActive(false);
            newObject.transform.SetParent(transform);
            newObject._stonesPool = this;
            return newObject;
        }

        public DropedStone GetObject()
        {
            var obj = _stonesPoolQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObject(DropedStone obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            _stonesPoolQueue.Enqueue(obj);
        }
    }
}
