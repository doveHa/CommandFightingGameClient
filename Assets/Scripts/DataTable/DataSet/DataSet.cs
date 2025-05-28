using UnityEngine;
using System.Collections.Generic;

namespace DataTable.DataSet
{
    public abstract class DataSet
    {
        public List<CharacterAllStatement> RawData { get; private set; }
        public Dictionary<string, FrameNumberDictionary> Statements { get; private set; }
        
        protected List<CharacterAllStatement> RawLeftSideData { get; set; }
        protected List<CharacterAllStatement> RawRightSideData { get; set; }
        protected Dictionary<string, FrameNumberDictionary> LeftSideStatements { get; set; }
        protected Dictionary<string, FrameNumberDictionary> RightSideStatements { get; set; }

        public static Vector2 FloatArrayToVector2(float[] array)
        {
            return new Vector2(array[0], array[1]);
        }

        public void SetLeftSide()
        {
            RawData = RawLeftSideData;
            Statements = LeftSideStatements;
        }

        public void SetRightSide()
        {
            RawData = RawRightSideData;
            Statements = RightSideStatements;
        }
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

    public class FrameNumberDictionary
    {
        public Dictionary<int, FrameData> Dictionary;

        public FrameNumberDictionary(List<FrameData> frameData)
        {
            Dictionary = new Dictionary<int, FrameData>();
            foreach (FrameData frame in frameData)
            {
                Dictionary.Add(frame.FrameNumber, frame);
            }
        }
    }
}