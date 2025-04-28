using System;
using System.Collections.Generic;

namespace Characters
{
    public interface ISkill
    {
        int AtkCoeff { get; set; }
        int HpCoeff { get; set; }
        int MoveSpeedCoeff { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int CoolTime { get; set; }
        List<string> Command { get; set; }

        Action<ISkill> Action { get; set; }
    }
}