using DataTable.DataSet;

namespace DataTable.FrameRanges
{
    public class KagetsuFrameRangesDictionary : FrameRangesDictionary
    {
        public KagetsuFrameRangesDictionary() : base()
        {
            AddRange("IttoRyotan", new int[] { 9, 15, 21, 22 });
            AddRange("NageKunai", new int[] { 8, 14, 19, 25 });
            AddRange("Nageru", new int[] { 4, 7, 16, 17 });
            AddRange("Sangiri", new int[] { 11, 21, 77, 78 });
        }
    }
}