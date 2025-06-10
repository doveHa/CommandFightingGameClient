namespace DataTable.FrameRanges
{
    public class NaktisFrameRangesDictionary : FrameRangesDictionary
    {
        public NaktisFrameRangesDictionary() : base()
        {
            AddRange("Idle", new int[] { 31, 62, 91, 121, 122 });
            AddRange("Walk", new int[] { 11, 21, 31, 41, 42 });
            AddRange("Jumping_Attack", new int[] { 7, 13, 19, 20 });
            AddRange("Jumping_Down", new int[] { 1, 2 });
            AddRange("Jumping_Up", new int[] { 2, 3, 4 });
            AddRange("Guard", new int[] { 1, 1 });
            AddRange("Hit", new int[] { 6, 9, 11 });
            AddRange("Airborne", new int[] { 0, 0, 0, 0 });
            AddRange("Atk_Punch", new int[] { 4, 19, 27, 28 });
            AddRange("Atk_Kick", new int[] { 7, 14, 20, 29, 36, 47, 47 });
            
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