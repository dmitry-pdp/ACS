namespace ACS.Model
{
    public class ColonyConfig
    {
        public byte StartAge;
        public ushort MaxAttributeValue;
        public ushort BaseAttributeDecay;
        public ushort BaseAttributeDecayDeviation;

        public RangeConstraint<ushort> WorkingAge;
        public RangeConstraint<ushort> BreedingAge;
    }
}
