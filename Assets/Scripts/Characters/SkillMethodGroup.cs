using System;
using System.Collections;
using System.Collections.Generic;
using RollbackNetCode;
using UnityEngine;

namespace Characters
{
    public static class SkillMethodGroup
    {
        public static Dictionary<string, Dictionary<string, Action<ISkill>>> actions = new()
        {
            {
                "c1", new Dictionary<string, Action<ISkill>>() { }
            },
            {
                "Naktis", new Dictionary<string, Action<ISkill>>
                {
                    { "Naktis s1", NaktisS1 },
                    { "Naktis s2", NaktisS2 },
                    { "Naktis s3", NaktisS3 },
                    { "Naktis s4", NaktisS4 }
                }
            },
            {
                "Kagetsu", new Dictionary<string, Action<ISkill>>
                {
                    { "Kagetsu s1", KagetsuS1 },
                    { "Kagetsu s2", KagetsuS2 },
                    { "Kagetsu s3", KagetsuS3 },
                    { "Kagetsu s4", KagetsuS4 }
                }
            },
            {
                "Vargon", new Dictionary<string, Action<ISkill>>
                {
                    { "Vargon s1", VargonS1 },
                    { "Vargon s2", VargonS2 },
                    { "Vargon s3", VargonS3 },
                    { "Vargon s4", VargonS4 }
                }
            }
        };

        public static void NaktisS1(ISkill skill)
        {
            Player player = GameManager.Manager.Player.GetComponent<Player>();
            player.GetComponentInChildren<Fly>().Run(skill);
        }

        //어퍼윙
        public static void NaktisS2(ISkill skill)
        {
            Debug.Log("NaktisS2");
        }

        //할퀴기
        public static void NaktisS3(ISkill skill)
        {
            Debug.Log("NaktisS3");
        }

        //바람 강타
        public static void NaktisS4(ISkill skill)
        {
            Debug.Log("NaktisS4");
        }

        public static void KagetsuS1(ISkill skill)
        {
            Debug.Log("KagetsuS1");
        }

        public static void KagetsuS2(ISkill skill)
        {
            Debug.Log("KagetsuS2");
        }

        public static void KagetsuS3(ISkill skill)
        {
            Debug.Log("KagetsuS3");
        }

        public static void KagetsuS4(ISkill skill)
        {
            Debug.Log("KagetsuS4");
        }

        public static void VargonS1(ISkill skill)
        {
            Debug.Log("VargonS1");
        }

        public static void VargonS2(ISkill skill)
        {
            Debug.Log("VargonS2");
        }

        public static void VargonS3(ISkill skill)
        {
            Debug.Log("VargonS3");
        }

        public static void VargonS4(ISkill skill)
        {
            Debug.Log("VargonS4");
        }
    }
}