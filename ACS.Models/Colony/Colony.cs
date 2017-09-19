namespace ACS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Colony
    {
        public Colonist[] Colonists;
        public List<ColonyModule> Modules;

        public void Tick()
        {
            this.ProcessProduction();
        }

        /* Returns power available for the given module. 
         * Power value is normalized to from 0 to 1000 and distributed evenly between modules.
         */
        public ushort GetPower(ColonyModule module)
        {
            throw new NotImplementedException();
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
