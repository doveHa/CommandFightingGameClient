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
            RawLeftSideData = JsonSerializer.Deserialize<List<CharacterAllStatement>>(
                File.ReadAllText("Assets/Data/HitBox/NaktisLeftSide.json"));
            LeftSideStatements = new Dictionary<string, FrameNumberDictionary>();
            foreach (CharacterAllStatement statement in RawLeftSideData)
            {
                LeftSideStatements.Add(statement.Statement, new FrameNumberDictionary(statement.FrameData));
            } 
            
            RawRightSideData = JsonSerializer.Deserialize<List<CharacterAllStatement>>(
                File.ReadAllText("Assets/Data/HitBox/NaktisRightSide.json"));
            RightSideStatements = new Dictionary<string, FrameNumberDictionary>();
            foreach (CharacterAllStatement statement in RawRightSideData)
            {
                RightSideStatements.Add(statement.Statement, new FrameNumberDictionary(statement.FrameData));
            }
            
        }
    }
}