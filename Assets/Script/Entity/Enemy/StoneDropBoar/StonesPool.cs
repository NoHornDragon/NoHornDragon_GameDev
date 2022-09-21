using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Entity.Enemy.stoneDropBoar
{
    public class StonesPool : MonoBehaviour
    {
        [SerializeField] private GameObject[] _stones;
        private int _currentStone;

        private void Start()
        {
            _currentStone = 0;
        }

        public GameObject InstantiateStone()
        {
            _currentStone++;
            if (_currentStone >= _stones.Length)
                _currentStone = 0;
            _stones[_currentStone].SetActive(true);
            return _stones[_currentStone];
        }
    }
}
