namespace DataTable.FrameRanges
{
    public class KagetsuFrameRangesDictionary : FrameRangesDictionary
    {
        public KagetsuFrameRangesDictionary() : base()
        {
            AddRange("Idle", new int[] { 41, 81, 121, 122 });
            AddRange("Walk", new int[] { 8, 16, 25, 34, 35 });
            AddRange("Jumping_Attack", new int[] { 6, 10, 13, 18, 24, 27, 31 });
            AddRange("Jumping_Down", new int[] { 4, 7, 8 });
            AddRange("Jumping_Up", new int[] { 5, 8, 12, 13 });
            AddRange("Guard", new int[] { 1, 1 });
            AddRange("Hit", new int[] { 13, 25, 26 });
            AddRange("Airborne", new int[] { 0, 0, 0, 0 });
            AddRange("Atk_Punch", new int[] { 3, 5, 8, 20, 22 });
            AddRange("Atk_Kick", new int[] { 5, 11, 15, 20, 26, 32, 40 });
            
            AddRange("IttoRyotan", new int[] { 6, 10, 16, 22, 28, 33, 38, 41, 45, 49 });
            AddRange("NageKunai", new int[] { 6, 10, 17, 22, 23 });
            AddRange("Nageru", new int[] { 8, 16, 21, 29, 37, 43 });
            AddRange("Sangiri", new int[] { 3, 10, 15, 21, 26, 31, 36, 42, 48, 53, 56, 62 });
        }
    }
}