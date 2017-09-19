namespace ACS.Model
{
    using System.Collections.Generic;

    public struct ColonyProductionInfoItem
    {
        public ColonyModule Module;
        public ResourceType ResourceType;
        public float Amount;
    }

    public class ColonyProductionInfo : List<ColonyProductionInfoItem>
    {
        public void Add(ColonyModule module, ResourceType resource, float amount)
        {
            this.Add(new ColonyProductionInfoItem
            {
                Module = module,
                ResourceType = resource,
                Amount = amount
            });
        }
    }
}
