namespace ACS.Model
{
    using System;
    using System.Collections.Generic;

    public class Colonist
    {
        public byte Age;

        public Dictionary<ColonistAttributeType, ColonistAttribute> Attributes;
        public ColonistTrait[] Traits;
        public ColonistState State;

        private ColonyConfig config;

        public Colonist(ColonyConfig config)
        {
            this.config = config;
            this.Age = config.StartAge;
            this.Attributes = new Dictionary<ColonistAttributeType, ColonistAttribute>
            {
                { ColonistAttributeType.Energy, new ColonistAttribute(ColonistAttributeType.Energy, config.MaxAttributeValue, config.MaxAttributeValue) },
                { ColonistAttributeType.Hunger, new ColonistAttribute(ColonistAttributeType.Hunger, config.MaxAttributeValue, config.MaxAttributeValue) },
                { ColonistAttributeType.Health, new ColonistAttribute(ColonistAttributeType.Health, config.MaxAttributeValue, config.MaxAttributeValue) },
                { ColonistAttributeType.Social, new ColonistAttribute(ColonistAttributeType.Social, config.MaxAttributeValue, config.MaxAttributeValue) },
                { ColonistAttributeType.Entertainment, new ColonistAttribute(ColonistAttributeType.Entertainment, config.MaxAttributeValue, config.MaxAttributeValue) }
            };

            this.State = ColonistState.Resting;
        }

        /*
        TODO: all attributes decay happening in the colony modules.
        public void ProcessColonistState()
        {
            this.Age++;
            this.Energy = this.DecayAttribute(this.Energy);
            this.Hunger = this.DecayAttribute(this.Hunger);
            this.Health = this.DecayAttribute(this.Health);
            this.Social = this.DecayAttribute(this.Social);
            this.Entertainment = this.DecayAttribute(this.Entertainment);
        }
        */

        public void DecayAttributes(WorkerAttributeDecay attributeDecayData)
        {
            if (attributeDecayData == null || attributeDecayData.Count == 0)
            {
                return;
            }

            foreach(var decayItem in attributeDecayData)
            {
                var attribute = this.Attributes[decayItem.Key];
                var decay = decayItem.Value.BaseDecay + EntropyGenerator.Decay(decayItem.Value.DecayDeviation);
                var newAttributeValue = (int)attribute.Value - (decayItem.Value.IsReverse ? -decay : decay);
                attribute.Value = Convert.ToUInt16(Math.Min(attribute.MaxValue, Math.Max(0, newAttributeValue)));
                this.Attributes[decayItem.Key] = attribute;
            }
        }
    }
}
