namespace ACS.Model
{
    using System.Collections.Generic;

    public class ResourceMineModule : ColonyModule
    {
        public List<KeyValuePair<ResourceType, ushort>> ResourceProductionRate;

        public ResourceMineModule(ColonyConfig config) : base(config)
        {
        }

        public override void Process(Colony colony, ColonyProductionInfo production)
        {
            if (this.ResourceProductionRate == null || this.ResourceProductionRate.Count == 0)
            {
                return;
            }

            var modifier = this.GetWorkerProductionModifier();

            foreach (var resource in this.ResourceProductionRate)
            {
                var producedResourceCount = resource.Value * modifier / 1000.0f;
                production.Add(this, resource.Key, producedResourceCount);
            }
        }
    }
}
