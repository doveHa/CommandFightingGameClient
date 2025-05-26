using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace DataTable.DataSet
{
    public class NaktisFrameDataSet : DataSet
    {
        public NaktisFrameDataSet()
        {
            RawDataSet = JsonSerializer.Deserialize<List<CharacterAllStatement>>(
                File.ReadAllText("Assets/Data/HitBox/Naktis.json"));
            Statements = new Dictionary<string, FrameNumberDictionary>();
            foreach (CharacterAllStatement statement in RawDataSet)
            {
                Statements.Add(statement.Statement, new FrameNumberDictionary(statement.FrameData));
            }
        }
    }
}