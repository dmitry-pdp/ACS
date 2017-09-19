namespace ACS.Model
{
    public class ColonyConfig
    {
        public byte StartAge;
        public ushort MaxAttributeValue;

        public RangeConstraint<ushort> WorkingAge;
        public RangeConstraint<ushort> BreedingAge;

        // Power production multiplier of workers capacity.
        public ushort PowerGeneratorColonyModule_WorkerModifier;
    }
}
