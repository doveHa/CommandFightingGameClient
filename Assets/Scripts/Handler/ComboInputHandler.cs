using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Handler
{
    public class ComboInputHandler : MonoBehaviour
    {
        private class ComboTireNode
        {
            public ISkill skill;

            //public Action<ISkill> Action;
            public readonly Dictionary<string, ComboTireNode> Children = new();
            public bool IsEndOfCombo;
        }

        private class ComboNode
        {
            public ComboTireNode TireNode;
            public float Time;
        }

        private const float TimeLimit = 2f;

        private readonly ComboTireNode _comboTireRoot = new();
        private readonly Queue<ComboNode> _inputQueue = new();

        private void AddCharacterCombo()
        {
            string characterName = CharacterManager.Manager.PlayerCharacterName;
            CharacterManager.Manager.CharacterGroup.Characters.TryGetValue(characterName, out ICharacter character);

            foreach (KeyValuePair<string, ISkill> skill in character.SkillGroup.Skills)
            {
                AddCombo(skill.Value);
            }
        }

        private void AddCombo(ISkill skill)
        {
            List<string> keySequence = skill.Command;
            Action<ISkill> action = skill.Action;
            ComboTireNode currentNode = _comboTireRoot;
            foreach (string key in keySequence)
            {
                if (currentNode.IsEndOfCombo) throw new UnReachableComboException();

                if (!currentNode.Children.ContainsKey(key))
                {
                    currentNode.Children.Add(key, new ComboTireNode());
                }

                currentNode = currentNode.Children[key];
            }

            currentNode.skill = skill;
            //currentNode.Action += action;
            currentNode.IsEndOfCombo = true;
            string sum = "";
            foreach (string node in keySequence)
            {
                sum += node;
            }

            Debug.Log(skill.Name + "등록 완료, 커맨드 > " + sum);
        }

        private void Start()
        {
            InputActionManager.Manager.Inputs.Command.CommandInput.performed += OnInputPerformed;
            AddCharacterCombo();
        }

        private void OnInputPerformed(InputAction.CallbackContext context)
        {
            string inputKey = context.control.name.ToUpper();
            ProcessInput(inputKey);
        }

        private void ProcessInput(string inputKey)
        {
            // 콤보 실행
            ComboTireNode nextTrieNode;
            foreach (ComboNode comboNode in _inputQueue)
            {
                if (!comboNode.TireNode.Children.TryGetValue(inputKey, out nextTrieNode))
                {
                    continue;
                }

                if (nextTrieNode.IsEndOfCombo)
                {
                    nextTrieNode.skill.Action.Invoke(nextTrieNode.skill);
                    //nextTrieNode.Action.Invoke();
                    _inputQueue.Clear();
                    return;
                }

                comboNode.TireNode = nextTrieNode;
                comboNode.Time = TimeLimit;
            }

            // 입력 값 큐에 기록 여부 판단
            if (!_comboTireRoot.Children.TryGetValue(inputKey, out nextTrieNode)) return;

            if (nextTrieNode.Children.Count > 0)
            {
                _inputQueue.Enqueue(new ComboNode { TireNode = nextTrieNode, Time = TimeLimit });
            }
        }
/*
        private void Update()
        {
            foreach (ComboNode comboNode in _inputQueue.ToList())
            {
                comboNode.Time -= Time.deltaTime;
                if (comboNode.Time <= 0)
                {
                    _inputQueue.Dequeue();
                }
            }
        }*/
    }

    public class UnReachableComboException : Exception
    {
        public UnReachableComboException() : base("Unreachable combo")
        {
        }
    }
}

/*
 *
        public class ComboTireNode
        {
            public Dictionary<string, ComboTireNode> Children = new();

            public ISkill Skill;

            //public Action Action;
            public bool IsEndOfCombo;
        }

        private const float TimeLimit = 2_000f;
        private const int MaxQueueSize = 4;

        private readonly ComboTireNode _comboTireRoot = new();

        // Queue 대신 배열과 인덱스를 사용하여 GC 부하 감소
        private readonly ComboTireNode[] _inputBuffer = new ComboTireNode[MaxQueueSize];
        private int _inputCount;

        private float _lastTimeCheck;

        private void AddCharacterCombo()
        {
            string characterName = CharacterManager.Manager.PlayerCharacterName;
            CharacterManager.Manager.CharacterGroup.Characters.TryGetValue(characterName, out ICharacter character);

            foreach (KeyValuePair<string, ISkill> skill in character.SkillGroup.Skills)
            {
                AddCombo(skill.Value);
            }
        }

        private void AddCombo(ISkill skill)
        {
            List<string> keySequence = skill.Command;

            ComboTireNode currentNode = _comboTireRoot;
            foreach (string key in keySequence)
            {
                if (currentNode.IsEndOfCombo) throw new UnReachableComboException();

                if (!currentNode.Children.TryGetValue(key, out ComboTireNode nextNode))
                {
                    // nextNode = GetNodeFromPool();
                    nextNode = new ComboTireNode();
                    currentNode.Children.Add(key, nextNode);
                }

                currentNode = nextNode;
            }

            currentNode.Skill = skill;
            currentNode.IsEndOfCombo = true;
        }

        public void OnInputPerformed(InputAction.CallbackContext ctx)
        {
            string context = ctx.control.name;
            Debug.Log(context);
            ProcessInput(context.ToUpper());
        }

        private void ProcessInput(string inputKey)
        {
            // long currentTime = Stopwatch.GetTimestamp();

            // 첫 입력이면 스톱워치 재시작
            if (_inputCount == 0)
            {
                _lastTimeCheck = Time.realtimeSinceStartup;
            }

            // 콤보 실행 및 체크
            int processedCount = 0;
            bool comboExecuted = false;

            for (int i = 0; i < _inputCount; i++)
            {
                ComboTireNode comboTireNode = _inputBuffer[i];

                if (comboTireNode.Children.TryGetValue(inputKey, out ComboTireNode nextTrieNode))
                {
                    if (nextTrieNode.IsEndOfCombo)
                    {
                        nextTrieNode.Skill.Action.Invoke(nextTrieNode.Skill);

                        // 플래그 설정 및 종료
                        comboExecuted = true;
                        break;
                    }

                    // 살아있는 콤보 노드는 다음 배열 위치에 저장
                    _inputBuffer[processedCount++] = nextTrieNode;
                }
            }

            // 콤보가 실행되었으면 모든 입력 초기화
            if (comboExecuted)
            {
                _inputCount = 0;
                return;
            }

            // 살아남은 노드 수 업데이트
            _inputCount = processedCount;

            // 새 입력이 루트에서 시작하는 유효한 콤보인지 확인
            if (_comboTireRoot.Children.TryGetValue(inputKey, out ComboTireNode rootNextNode))
            {
                // 콤보 노드가 자식을 가지고 있으면 추가
                if (rootNextNode.Children.Count > 0)
                {
                    if (_inputCount >= MaxQueueSize)
                    {
                        // 가장 오래된 입력 제거
                        Array.Copy(_inputBuffer, 1, _inputBuffer, 0, MaxQueueSize - 1);
                        _inputCount--;
                    }

                    _inputBuffer[_inputCount++] = rootNextNode;
                }
            }
        }

        private void Awake()
        {
            InputActionManager.Manager.Inputs.Command.CommandInput.performed += OnInputPerformed;
            AddCharacterCombo();
        }

    }



*/