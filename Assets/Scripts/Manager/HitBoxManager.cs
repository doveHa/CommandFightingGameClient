using System.Collections.Generic;
using DataTable.DataSet;
using UnityEngine;

namespace Manager
{
    public class HitBoxManager : MonoBehaviour
    {
        public static HitBoxManager Manager;

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }

        public string PlayerCurrentState { get; set; }
        public string OpponentCurrentState { get; set; }

        private List<DataSet.FrameData> playerCurrentFrameData;
        private List<DataSet.FrameData> opponentCurrentFrameData;

        public void SetPlayerState(string state)
        {
            PlayerCurrentState = state;
        }
        public void HitJudgement()
        {
            
        }
    }
}