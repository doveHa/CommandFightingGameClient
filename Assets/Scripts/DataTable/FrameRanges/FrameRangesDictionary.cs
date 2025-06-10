using System.Collections.Generic;

namespace DataTable.FrameRanges
{
    public abstract class FrameRangesDictionary
    {
        public Dictionary<string, List<FrameRange>> FrameRanges { get; private set; }

        public FrameRangesDictionary()
        {
            FrameRanges = new Dictionary<string, List<FrameRange>>();
        }

        protected void AddRange(string state, int[] ranges)
        {
            List<FrameRange> frameRanges = new List<FrameRange>();
            int pastRange = -1;
            foreach (int range in ranges)
            {
                frameRanges.Add(new FrameRange(pastRange + 1, range));
                pastRange = range;
            }

            FrameRanges.Add(state, frameRanges);
        }
    }

    public struct FrameRange
    {
        public FrameRange(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public int start;
        public int end;
    }
}