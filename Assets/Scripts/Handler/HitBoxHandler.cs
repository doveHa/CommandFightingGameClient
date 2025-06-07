using System.Collections.Generic;
using DataTable.DataSet;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class HitBoxHandler : MonoBehaviour
    {
        [SerializeField] private Player currentPlayer;
        [SerializeField] private Player opponentPlayer;
        private string currentState;
        private string opponentState;

        private FrameNumberDictionary currentFrameData;
        private FrameNumberDictionary opponentCurrentFrameData;

        private int currentFrameIndex, opponentFrameIndex;

        public bool isPlayer;

        void Start()
        {
            isPlayer = CompareTag("Player");
        }

        public void SetCurrentState(string state, int frameIndex)
        {
            currentState = state;
            currentFrameData = currentPlayer.DataSet.Statements[state];
            currentFrameIndex = frameIndex;
        }

        public void SetOpponentState(string state, int frameIndex)
        {
            opponentState = state;
            opponentCurrentFrameData = opponentPlayer.DataSet.Statements[state];
            opponentFrameIndex = frameIndex;
        }

        void FixedUpdate()
        {
            if (currentFrameData != null && opponentCurrentFrameData != null)
            {
                Debug.Log(currentState);
                var playerFrame = currentFrameData.Dictionary[currentFrameIndex + 1];
                Debug.Log(opponentState);
                var opponentFrame = opponentCurrentFrameData.Dictionary[opponentFrameIndex + 1];

                if (isPlayer)
                {
                    GameManager.Manager.SetPlayerCenter(DataSet.FloatArrayToVector2(playerFrame.Center));
                }
                else
                {
                    GameManager.Manager.SetOpponentCenter(DataSet.FloatArrayToVector2(playerFrame.Center));
                }

                foreach (var playerBox in playerFrame.HurtBoxes)
                {
                    int stateToSkill = VarManager.StateToSkill(currentState);

                    if (stateToSkill == -1)
                    {
                        return;
                    }

                    if (playerBox.PartName.Equals("HitBox"))
                    {
                        Vector2 playerCenter =
                            (Vector2)currentPlayer.transform.GetChild(0).GetChild(0).position +
                            DataSet.FloatArrayToVector2(playerBox.OffSet);
                        Vector2 playerSize = DataSet.FloatArrayToVector2(playerBox.Size);
                        Rect playerHitRect = new Rect(playerCenter - playerSize / 2f, playerSize);

                        foreach (var opponentBox in opponentFrame.HurtBoxes)
                        {
                            Vector2 opponentCenter =
                                (Vector2)opponentPlayer.transform.GetChild(0).GetChild(0)
                                    .position +
                                DataSet.FloatArrayToVector2(opponentBox.OffSet);
                            Vector2 opponentSize = DataSet.FloatArrayToVector2(opponentBox.Size);
                            Rect opponentHurtRect = new Rect(opponentCenter - opponentSize / 2f, opponentSize);
                            ICharacterSkill skill = GetHitSkill(stateToSkill);
                            if (!skill.HasHit && playerHitRect.Overlaps(opponentHurtRect))
                            {
                                Debug.Log("Hit Detected!");
                                skill.HasHit = true;
                                opponentPlayer.Hit(skill.Damage, skill.CommandLength);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private ICharacterSkill GetHitSkill(int skillIndex)
        {
            if (isPlayer)
            {
                return VarManager.Manager.PlayerSkills[skillIndex];
            }

            return VarManager.Manager.OpponentSkills[skillIndex];
        }
    }
}