using System.Collections.Generic;

namespace DataTable.FrameRanges
{
    public abstract class FrameRangesDictionary
    {
        public Dictionary<string, List<FrameRange>> FrameRanges { get; private set; }

        public FrameRangesDictionary()
        {
            FrameRanges = new Dictionary<string, List<FrameRange>>();
            AddRange("Idle", new int[]{30,60,90,120, 121});
            AddRange("Walk", new int[]{10,20,30,40, 41});
            AddRange("Jumping_Attack", new int[]{0,0,0,0});
            AddRange("Jumping_Down", new int[]{0,0,0,0});
            AddRange("Jumping_Up", new int[]{0,0,0,0});
            AddRange("Guard", new int[]{0,0,0,0});
            AddRange("Hit", new int[]{0,0,0,0});
            AddRange("Airborne", new int[]{0,0,0,0});
            AddRange("Atk_Punch", new int[]{8,16,24,32});
            AddRange("Atk_Kick", new int[]{0,0,0,0});
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