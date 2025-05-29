using System;
using System.Collections.Generic;
using Characters.Skill.Naktis;
using UnityEngine;

namespace Characters
{
    public static class SkillMethodGroup
    {
        public static Dictionary<string, Dictionary<string, Action<SkillInfo>>> actions = new()
        {
            {
                "c1", new Dictionary<string, Action<SkillInfo>>() { }
            },
            {
                "Naktis", new Dictionary<string, Action<SkillInfo>>
                {
                    { "비행", NaktisS1 },
                    { "바람강타", NaktisS2 },
                    { "할퀴기", NaktisS3 },
                    { "어퍼윙", NaktisS4 }
                }
            },
            {
                "Kagetsu", new Dictionary<string, Action<SkillInfo>>
                {
                    { "Kagetsu s1", KagetsuS1 },
                    { "Kagetsu s2", KagetsuS2 },
                    { "Kagetsu s3", KagetsuS3 },
                    { "Kagetsu s4", KagetsuS4 }
                }
            },
            {
                "Vargon", new Dictionary<string, Action<SkillInfo>>
                {
                    { "Vargon s1", VargonS1 },
                    { "Vargon s2", VargonS2 },
                    { "Vargon s3", VargonS3 },
                    { "Vargon s4", VargonS4 }
                }
            }
        };

        public static void NaktisS1(SkillInfo skillInfo)
        {
            GameObject.FindWithTag("Player").GetComponentInChildren<Fly>().Run();
            //GameManager.Manager.Player.GetComponentInChildren<Player>().GetComponentInChildren<Fly>().Run();
        }

        //바람 강타
        public static void NaktisS2(SkillInfo skillInfo)
        {
            GameObject.FindWithTag("Player").GetComponentInChildren<Hasegi>().Run();

            //GameManager.Manager.Player.GetComponentInChildren<Player>().GetComponentInChildren<Hasegi>().Run();
        }

        //할퀴기
        public static void NaktisS3(SkillInfo skillInfo)
        {
            GameObject.FindWithTag("Player").GetComponentInChildren<Scratch>().Run();

            //GameManager.Manager.Player.GetComponentInChildren<Player>().GetComponentInChildren<Scratch>().Run();
        }

        //어퍼윙
        public static void NaktisS4(SkillInfo skillInfo)
        {
            GameObject.FindWithTag("Player").GetComponentInChildren<UpperWing>().Run();

            //GameManager.Manager.Player.GetComponentInChildren<Player>().GetComponentInChildren<UpperWing>().Run();
        }

        public static void KagetsuS1(SkillInfo skillInfo)
        {
            Debug.Log("KagetsuS1");
        }

        public static void KagetsuS2(SkillInfo skillInfo)
        {
            Debug.Log("KagetsuS2");
        }

        public static void KagetsuS3(SkillInfo skillInfo)
        {
            Debug.Log("KagetsuS3");
        }

        public static void KagetsuS4(SkillInfo skillInfo)
        {
            Debug.Log("KagetsuS4");
        }

        public static void VargonS1(SkillInfo skillInfo)
        {
            Debug.Log("VargonS1");
        }

        public static void VargonS2(SkillInfo skillInfo)
        {
            Debug.Log("VargonS2");
        }

        public static void VargonS3(SkillInfo skillInfo)
        {
            Debug.Log("VargonS3");
        }

        public static void VargonS4(SkillInfo skillInfo)
        {
            Debug.Log("VargonS4");
        }

        private static void SetStatement()
        {
            
        }
    }
}