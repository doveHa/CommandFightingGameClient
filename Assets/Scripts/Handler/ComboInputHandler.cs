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

        private void Awake()
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
        }
    }
}


public class UnReachableComboException : Exception
{
    public UnReachableComboException() : base("Unreachable combo")
    {
    }
}