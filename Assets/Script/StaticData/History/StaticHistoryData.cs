using System.Collections.Generic;
using UnityEngine;

namespace NHD.StaticData.History
{
    public class StaticHistoryData : MonoBehaviour
    {
        public static PaperNode[] _nodes;
        public static bool[] _isGetPapers;
        public static int _totalPlayTime;
        public static int _restartCount;
        public static int _stunCount;
    }
}