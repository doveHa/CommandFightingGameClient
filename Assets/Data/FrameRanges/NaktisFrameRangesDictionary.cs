using System.Collections.Generic;
using DataTable;

namespace Data.FrameRanges
{
    public class NaktisFrameRangesDictionary : FrameRangesDictionary
    {
        protected override void Start()
        {
            NaktisFrameDataSet.Start();
        }

        public NaktisFrameRangesDictionary() : base()
        {
            AddRange(new int[] { 0, 0, 0, 0 }, "Hasegi");
            AddRange(new int[] { 0, 0, 0, 0 }, "Scratch");
            AddRange(new int[] { 0, 0, 0, 0 }, "UpperWing");
            AddRange(new int[] { 10, 20, 30, 40 }, "Fly_Drop");
            AddRange(new int[] { 15, 30, 45, 60, 75 }, "Fly_Standing");
            AddRange(new int[] { 5, 15, 25, 30 }, "Fly_Up");
            AddRange(new int[] { 0, 0, 0, 0 }, "Fly_WalkLeft");
            AddRange(new int[] { 0, 0, 0, 0 }, "Fly_WalkRight");
        }
    }
}