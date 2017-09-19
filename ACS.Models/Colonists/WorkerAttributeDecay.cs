namespace ACS.Model
{
    using System.Collections.Generic;

    public struct WorkerAttributeDecayItem
    {
        public ColonistAttributeType Type;
        public ushort BaseDecay;
        public ushort DecayDeviation;
        public bool IsReverse;
    }

    public class WorkerAttributeDecay : Dictionary<ColonistAttributeType, WorkerAttributeDecayItem>
    {
    }
}
