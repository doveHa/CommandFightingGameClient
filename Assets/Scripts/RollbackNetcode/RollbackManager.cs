using System.Collections.Generic;
using Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace RollbackNetcode
{
    public class RollbackManager : MonoBehaviour
    {
        public static RollbackManager Manager { get; private set; }
        public int CurrentFrame { get; private set; } = 1;

        public Simulator LocalSimulator { get; private set; }
        public Simulator RemoteSimulator { get; private set; }

        private Dictionary<int, Vector2> localPositions, remotePositions;

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }

            LocalSimulator = new Simulator(true);
            RemoteSimulator = new Simulator(false);

            localPositions = new Dictionary<int, Vector2>();
            remotePositions = new Dictionary<int, Vector2>();
        }

        void Start()
        {
            LocalSimulator.MoveStates.Add(0, new MoveState());
            LocalSimulator.JumpStates.Add(0, new JumpState());
            LocalSimulator.ActiveStates.Add(0, new ActiveState());
            RemoteSimulator.MoveStates.Add(0, new MoveState());
            RemoteSimulator.JumpStates.Add(0, new JumpState());
            RemoteSimulator.ActiveStates.Add(0, new ActiveState());

            localPositions.Add(0, VarManager.Manager.PlayerGameObject.transform.position);
            remotePositions.Add(0, VarManager.Manager.OpponentGameObject.transform.position);
        }

        void FixedUpdate()
        {
            if (!RemoteSimulator.ActiveStates.ContainsKey(CurrentFrame))
            {
                //행동 예측은 None
                RemoteSimulator.ActiveStates.Add(CurrentFrame, new ActiveState());
            }

            if (!RemoteSimulator.MoveStates.ContainsKey(CurrentFrame))
            {
                //이동 예측은 전 프레임과 동일
                RemoteSimulator.MoveStates.Add(CurrentFrame, RemoteSimulator.MoveStates[CurrentFrame - 1].Clone());
            }

            if (!RemoteSimulator.JumpStates.ContainsKey(CurrentFrame))
            {
                //점프 예측은 false
                RemoteSimulator.JumpStates.Add(CurrentFrame, new JumpState());
            }

            LocalSimulator.Simulate(CurrentFrame);
            RemoteSimulator.Simulate(CurrentFrame);

            localPositions.Add(CurrentFrame, GameObject.Find("Player").transform.GetChild(0).transform.position);
            remotePositions.Add(CurrentFrame, GameObject.Find("Opponent").transform.GetChild(0).transform.position);
            CurrentFrame++;
            /*
             지연방식
            if (CurrentFrame < 5)
            {
                CurrentFrame++;
            }
            else
            {
                Active.Simulate(CurrentFrame - 5);
                Jump.Simulate(CurrentFrame - 5);
                Move.Simulate(CurrentFrame - 5);
                CurrentFrame++;
            }*/
        }

        private const int FRAME = 0, MOVE = 1, JUMP = 2, ACTIVE = 3;
        private int SimulateDoneFrame = 0;

        public void ProcessingMessage(string msg)
        {
            string[] split = msg.Split(Constant.SteamNetworkingType.DELIMITER);
            int frame = int.Parse(split[FRAME]);

            //Debug.Log(msg);
            RemoteSimulator.ActiveStates[frame] = new ActiveState(int.Parse(split[ACTIVE]));
            //Debug.Log($"[{frame}] ProcessingMessage's MoveDirection {split[MOVE]}");
            RemoteSimulator.MoveStates[frame] = new MoveState(int.Parse(split[MOVE]));
            //RemoteSimulator.MoveStates[frame].Print();
            RemoteSimulator.JumpStates[frame] = new JumpState(bool.Parse(split[JUMP]));

            if (frame < CurrentFrame)
            {
                Debug.Log($"Receive frame {frame}, CurrentFrame{CurrentFrame}");
                RestoreState(frame);
            }
        }

        private void RestoreState(int frame)
        {
            //VarManager.Manager.OpponentGameObject.transform.position = remotePositions[frame - 1];
            for (int i = SimulateDoneFrame; i < frame; i++)
            {
                Debug.Log($"[{i}] Restore Start");
                RemoteSimulator.Simulate(i);
                SimulateDoneFrame = frame;
            }
        }
    }
}