using UnityEngine;

namespace NHD.GamePlay.CameraComponent
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _stageList;

        private void Awake()
        {
            for(int i = 0; i < _stageList.Length; i++)
            {
               _stageList[i].SetActive(false);
            }
        }


        /// <param name="inputStageNumber">스테이지 번호</param>
        /// <param name="isIn">플레이어가 들어오면 true, 아니면 false</param>
        public void StageChange(int inputStageNumber, bool isIn)
        {
            int index = inputStageNumber - 1;

            if (isIn)
            {
                _stageList[index].SetActive(true);
                
                // active near stage
                if(index + 1 < _stageList.Length)
                    _stageList[index + 1].SetActive(true);
                
                if(index - 1 >= 0)
                    _stageList[index - 1].SetActive(true);
            }
            
            // deactive stage
            if(index - 2 >= 0)
                _stageList[index - 2].SetActive(false);
            if(index + 2 < _stageList.Length)
                _stageList[index + 2].SetActive(false);
        }
    }
}