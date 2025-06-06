using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DataTable.DataSet
{
    public class KagetsuFrameDataSet : DataSet
    {
        public KagetsuFrameDataSet()
        {
            RawLeftSideData = JsonSerializer.Deserialize<List<CharacterAllStatement>>(
                File.ReadAllText("Assets/Data/HitBox/KagetsuLeftSide.json"));
            LeftSideStatements = new Dictionary<string, FrameNumberDictionary>();
            foreach (CharacterAllStatement statement in RawLeftSideData)
            {
                LeftSideStatements.Add(statement.Statement, new FrameNumberDictionary(statement.FrameData));
            }

            RawRightSideData = JsonSerializer.Deserialize<List<CharacterAllStatement>>(
                File.ReadAllText("Assets/Data/HitBox/KagetsuRightSide.json"));
            RightSideStatements = new Dictionary<string, FrameNumberDictionary>();
            foreach (CharacterAllStatement statement in RawRightSideData)
            {
                RightSideStatements.Add(statement.Statement, new FrameNumberDictionary(statement.FrameData));
            }
        }
        
    }
}