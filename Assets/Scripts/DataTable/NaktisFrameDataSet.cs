using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace DataTable
{
    public static class NaktisFrameDataSet
    {
        public static List<CharacterAllStatement> DataSet { get; set; }
        public static Dictionary<string, List<FrameData>> Statements { get; private set; }


        public static void Start()
        {
            DataSet = JsonSerializer.Deserialize<List<CharacterAllStatement>>(
                File.ReadAllText("Assets/Data/HitBox/Naktis.json"));
            Debug.Log(File.ReadAllText("Assets/Data/HitBox/Naktis.json"));
            Statements = new Dictionary<string, List<FrameData>>();
            foreach (CharacterAllStatement statement in DataSet)
            {
                Statements.Add(statement.Statement, statement.FrameData);
            }
        }

        public static Vector2 FloatArrayToVector2(float[] array)
        {
            return new Vector2(array[0], array[1]);
        }
    }

    public class HurtBox
    {
        public string PartName { get; set; }
        public float[] OffSet { get; set; }
        public float[] Size { get; set; }
    }

    public class FrameData
    {
        public int FrameNumber { get; set; }
        public float[] Center { get; set; }
        public List<HurtBox> HurtBoxes { get; set; }
    }


    public class CharacterAllStatement
    {
        public string Statement { get; set; }
        public List<FrameData> FrameData { get; set; }
    }
}