using System;
using System.Collections.Generic;
using Characters;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Handler
{
    public class ComboInputHandler : MonoBehaviour
    {
        public class ComboTireNode
        {
            public Dictionary<string, ComboTireNode> Children = new();
            public ISkill Skill;
            public bool IsEndOfCombo;
        }

        private struct TimedComboNode
        {
            public ComboTireNode Node;
            public float ExpireTime;
        }

        private const float TimeLimit = 2_000f; // 밀리초
        private const int MaxQueueSize = 4;

        private readonly ComboTireNode _comboTireRoot = new();
        private readonly TimedComboNode[] _inputBuffer = new TimedComboNode[MaxQueueSize];
        private int _inputCount;

        private void Start()
        {
            InputActionManager.Manager.Inputs.Command.CommandInput.performed += OnInputPerformed;
        }

        public void AddCharacterCombo()
        {
            string characterName = CharacterManager.Manager.PlayerCharacterName;
            CharacterManager.Manager.CharacterGroup.Characters.TryGetValue(characterName, out ICharacter character);

            foreach (KeyValuePair<string, ISkill> skill in character.SkillGroup.Skills)
            {
                AddCombo(skill.Value);
            }
        }

        public void AddCombo(ISkill skill)
        {
            List<string> keySequence = skill.Command;
            ComboTireNode currentNode = _comboTireRoot;

            foreach (string key in keySequence)
            {
                if (currentNode.IsEndOfCombo) throw new UnReachableComboException();

                if (!currentNode.Children.TryGetValue(key, out ComboTireNode nextNode))
                {
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
            float currentTime = Time.time;
            float expirationTime = currentTime + (TimeLimit / 1000f);

            int processedCount = 0;
            bool comboExecuted = false;

            for (int i = 0; i < _inputCount; i++)
            {
                TimedComboNode timedNode = _inputBuffer[i];

                if (timedNode.Node.Children.TryGetValue(inputKey, out ComboTireNode nextNode))
                {
                    if (nextNode.IsEndOfCombo)
                    {
                        nextNode.Skill.Action.Invoke(nextNode.Skill);
                        comboExecuted = true;
                        break;
                    }

                    _inputBuffer[processedCount++] = new TimedComboNode
                    {
                        Node = nextNode,
                        ExpireTime = expirationTime
                    };
                }
            }

            if (comboExecuted)
            {
                _inputCount = 0;
                return;
            }

            _inputCount = processedCount;

            if (_comboTireRoot.Children.TryGetValue(inputKey, out ComboTireNode rootNextNode))
            {
                if (rootNextNode.Children.Count > 0)
                {
                    if (_inputCount >= MaxQueueSize)
                    {
                        Array.Copy(_inputBuffer, 1, _inputBuffer, 0, MaxQueueSize - 1);
                        _inputCount--;
                    }

                    _inputBuffer[_inputCount++] = new TimedComboNode
                    {
                        Node = rootNextNode,
                        ExpireTime = expirationTime
                    };
                }
            }
        }

        private void Update()
        {
            float currentTime = Time.time;
            int writeIndex = 0;

            for (int i = 0; i < _inputCount; i++)
            {
                if (_inputBuffer[i].ExpireTime > currentTime)
                {
                    _inputBuffer[writeIndex++] = _inputBuffer[i];
                }
            }

            _inputCount = writeIndex;
        }

        public class UnReachableComboException : Exception
        {
            public UnReachableComboException() : base("Unreachable combo") { }
        }
    }
}
