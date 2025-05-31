using System.Collections.Generic;
using DataTable;

namespace Data.FrameRanges
{
    public class NaktisFrameRangesDictionary : FrameRangesDictionary
    {
        public NaktisFrameRangesDictionary() : base()
        {
            AddRange(new int[] { 6, 14, 21, 28 }, "Hasegi");
            AddRange(new int[] { 8, 20, 28, 35 }, "Scratch");
            AddRange(new int[] { 10, 20, 30, 31 }, "UpperWing");
            AddRange(new int[] { 10, 20, 30, 31 }, "Fly_Drop");
            AddRange(new int[] { 15, 30, 45, 61, 62 }, "Fly_Standing");
            AddRange(new int[] { 5, 15, 25, 26 }, "Fly_Up");
            AddRange(new int[] { 0, 0, 0, 0 }, "Fly_WalkLeft");
            AddRange(new int[] { 0, 0, 0, 0 }, "Fly_WalkRight");
        }
    }
}