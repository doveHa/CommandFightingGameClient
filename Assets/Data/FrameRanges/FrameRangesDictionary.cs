using System.Collections.Generic;
using Unity.VisualScripting;

namespace Data.FrameRanges
{
    public abstract class FrameRangesDictionary
    {
        public Dictionary<string, List<FrameRange>> FrameRanges { get; private set; }

        public FrameRangesDictionary()
        {
            FrameRanges = new Dictionary<string, List<FrameRange>>();
            AddRange(new int[]{30,60,90,120, 121},"Idle");
            AddRange(new int[]{10,20,30,40, 41},"Walk");
            AddRange(new int[]{0,0,0,0},"Jumping_Attack");
            AddRange(new int[]{0,0,0,0},"Jumping_Down");
            AddRange(new int[]{0,0,0,0},"Jumping_Up");
            AddRange(new int[]{0,0,0,0},"Guard");
            AddRange(new int[]{0,0,0,0},"Hit");
            AddRange(new int[]{0,0,0,0},"Airborne");
            AddRange(new int[]{8,16,24,32},"Atk_Punch");
            AddRange(new int[]{0,0,0,0},"Atk_Kick");
        }
        
        protected void AddRange(int[] ranges, string state)
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