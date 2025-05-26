using UnityEngine;
using System.Collections.Generic;

namespace DataTable.DataSet
{
    public abstract class DataSet
    {
        public static List<CharacterAllStatement> RawDataSet { get; set; }
        public static Dictionary<string, List<FrameData>> Statements { get; set; }

        public static Vector2 FloatArrayToVector2(float[] array)
        {
            return new Vector2(array[0], array[1]);
        }

        public class HurtBox
        {
            //hit box
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
}