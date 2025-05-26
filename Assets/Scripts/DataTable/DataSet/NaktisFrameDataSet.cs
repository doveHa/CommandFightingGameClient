using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace DataTable.DataSet
{
    public class NaktisFrameDataSet : DataSet
    {
        public static void GetFrameData()
        {
            RawDataSet = JsonSerializer.Deserialize<List<CharacterAllStatement>>(
                File.ReadAllText("Assets/Data/HitBox/Naktis.json"));
            Statements = new Dictionary<string, List<FrameData>>();
            foreach (CharacterAllStatement statement in RawDataSet)
            {
                Statements.Add(statement.Statement, statement.FrameData);
            }
        }

    }

}