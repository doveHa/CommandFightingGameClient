using DataTable.DataSet;

namespace DataTable.FrameRanges
{
    public class NaktisFrameRangesDictionary : FrameRangesDictionary
    {
        public NaktisFrameRangesDictionary() : base()
        {
            AddRange("Hasegi", new int[] { 9, 15, 21, 22 });
            AddRange("Scratch", new int[] { 8, 14, 19, 25 });
            AddRange("UpperWing", new int[] { 4, 7, 16, 17 });
            AddRange("Fly_Drop", new int[] { 11, 21, 77, 78 });
            AddRange("Fly_Standing", new int[] { 16, 31, 46, 62, 63 });
            AddRange("Fly_Up", new int[] { 6, 16, 26, 27 });
            AddRange("Fly_WalkLeft", new int[] { 0, 0, 0, 0 });
            AddRange("Fly_WalkRight", new int[] { 0, 0, 0, 0 });
        }
    }
}