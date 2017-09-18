namespace ACS.Model
{
    using System;
    using System.Collections.Generic;

    public class Colony
    {
        public Colonist[] Colonists;
        public List<ColonyModule> Modules;

        public void Tick()
        {
            this.ProcessProduction();
        }

        private void ProcessProduction()
        {
            var productionData = new ColonyProductionInfo();

            foreach (var module in this.Modules)
            {
                module.Process(this, productionData);
            }
        }
    }
}
