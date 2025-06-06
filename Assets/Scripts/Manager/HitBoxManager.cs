using System.Collections.Generic;
using System.Globalization;
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

        private FrameNumberDictionary playerCurrentFrameData;
        private FrameNumberDictionary opponentCurrentFrameData;

        private int playerFrameIndex, opponentFrameIndex;

        public void SetPlayerState(string state, int frameIndex)
        {
            PlayerCurrentState = state;
            playerCurrentFrameData = VarManager.Manager.Player.DataSet.Statements[state];
            playerFrameIndex = frameIndex;
        }

        public void SetOpponentState(string state, int frameIndex)
        {
            OpponentCurrentState = state;
            opponentCurrentFrameData = VarManager.Manager.Opponent.DataSet.Statements[state];
            opponentFrameIndex = frameIndex;
        }

        void FixedUpdate()
        {
            if (playerCurrentFrameData != null && opponentCurrentFrameData != null)
            {
                var playerFrame = playerCurrentFrameData.Dictionary[playerFrameIndex + 1];
                var opponentFrame = opponentCurrentFrameData.Dictionary[opponentFrameIndex + 1];
                foreach (var playerBox in playerFrame.HurtBoxes)
                {
                    if (playerBox.PartName.Equals("HitBox"))
                    {
                        Vector2 playerCenter =
                            (Vector2)VarManager.Manager.PlayerGameObject.transform.GetChild(0).GetChild(0).position +
                            DataSet.FloatArrayToVector2(playerBox.OffSet);
                        Vector2 playerSize = DataSet.FloatArrayToVector2(playerBox.Size);
                        Rect playerHitRect = new Rect(playerCenter - playerSize / 2f, playerSize);

                        foreach (var opponentBox in opponentFrame.HurtBoxes)
                        {
                            Vector2 opponentCenter =
                                (Vector2)VarManager.Manager.OpponentGameObject.transform.GetChild(0).GetChild(0)
                                    .position +
                                DataSet.FloatArrayToVector2(opponentBox.OffSet);
                            Vector2 opponentSize = DataSet.FloatArrayToVector2(opponentBox.Size);
                            Rect opponentHurtRect = new Rect(opponentCenter - opponentSize / 2f, opponentSize);
                            ICharacterSkill skill = GetHitSkill();
                            if (!skill.HasHit && playerHitRect.Overlaps(opponentHurtRect))
                            {
                                Debug.Log("Hit Detected!");
                                GetHitSkill().Hit();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private ICharacterSkill GetHitSkill()
        {
            return VarManager.Manager.PlayerSkills[VarManager.SkillMapping(PlayerCurrentState)];
        }

        
    }
}