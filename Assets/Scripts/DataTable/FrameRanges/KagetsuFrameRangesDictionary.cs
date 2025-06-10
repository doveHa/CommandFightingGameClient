using DataTable.DataSet;

namespace DataTable.FrameRanges
{
    public class KagetsuFrameRangesDictionary : FrameRangesDictionary
    {
        public KagetsuFrameRangesDictionary() : base()
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
            
            AddRange("IttoRyotan", new int[] { 9, 15, 21, 22 });
            AddRange("NageKunai", new int[] { 8, 14, 19, 25 });
            AddRange("Nageru", new int[] { 4, 7, 16, 17 });
            AddRange("Sangiri", new int[] { 11, 21, 77, 78 });
        }
    }
}