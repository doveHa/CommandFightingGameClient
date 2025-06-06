using System.Collections.Generic;

namespace DataTable.FrameRanges
{
    public abstract class FrameRangesDictionary
    {
        public Dictionary<string, List<FrameRange>> FrameRanges { get; private set; }

        public FrameRangesDictionary()
        {
            FrameRanges = new Dictionary<string, List<FrameRange>>();
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