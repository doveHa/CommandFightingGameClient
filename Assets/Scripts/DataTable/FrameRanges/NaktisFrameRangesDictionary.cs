using DataTable.DataSet;

namespace DataTable.FrameRanges
{
    public class NaktisFrameRangesDictionary : FrameRangesDictionary
    {
        public NaktisFrameRangesDictionary() : base()
        {
            AddRange("Hasegi", new int[] { 6, 14, 21, 28 });
            AddRange("Scratch", new int[] { 8, 20, 28, 35 });
            AddRange("UpperWing", new int[] { 5, 10, 20, 21 });
            AddRange("Fly_Drop", new int[] { 10, 20, 30, 31 });
            AddRange("Fly_Standing", new int[] { 15, 30, 45, 61, 62 });
            AddRange("Fly_Up", new int[] { 5, 15, 25, 26 });
            AddRange("Fly_WalkLeft", new int[] { 0, 0, 0, 0 });
            AddRange("Fly_WalkRight", new int[] { 0, 0, 0, 0 });
        }
    }
}