namespace ACS.Model
{
    using System;

    public class Colonist
    {
        public byte Age;
        public ushort Energy;
        public ushort Hunger;
        public ushort Health;
        public ushort Social;
        public ushort Entertainment;
        public ColonistTrait[] Traits;
        public ColonistState State;

        private ColonyConfig config;

        public Colonist(ColonyConfig config)
        {
            this.config = config;
            this.Age = config.StartAge;
            this.Energy = config.MaxAttributeValue;
            this.Hunger = config.MaxAttributeValue;
            this.Health = config.MaxAttributeValue;
            this.Social = config.MaxAttributeValue;
            this.Entertainment = config.MaxAttributeValue;
            this.State = ColonistState.Resting;
        }

        public void ProcessColonistState()
        {
            this.Age++;
            this.Energy = this.DecayAttribute(this.Energy);
            this.Hunger = this.DecayAttribute(this.Hunger);
            this.Health = this.DecayAttribute(this.Health);
            this.Social = this.DecayAttribute(this.Social);
            this.Entertainment = this.DecayAttribute(this.Entertainment);
        }
        
        private ushort DecayAttribute(ushort attributeValue)
        {
            var decay = this.config.BaseAttributeDecay + EntropyGenerator.Decay(this.config.BaseAttributeDecayDeviation);
            var newAttributeValue = attributeValue - decay;
            return Convert.ToUInt16(Math.Max(0, newAttributeValue));
        }
    }
}
