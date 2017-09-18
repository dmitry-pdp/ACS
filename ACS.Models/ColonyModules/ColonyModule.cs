using System;
namespace ACS.Model
{
    /* Colony modules are responsible for colony grows and resource production.
     * Every module has traits which benefits its yield if worker is assigned to this module.
     * Trait modifiers are in thousands of unit of production.
     */

    using System.Collections.Generic;
    using System.Linq;

    public abstract class ColonyModule
    {
        public ushort Capacity;
        public List<Colonist> Workers;
        public Dictionary<ColonistTrait, ushort> TraitsModifiers;

        protected ColonyConfig config;

        public ColonyModule(ColonyConfig config)
        {
            this.config = config;
        }

        public abstract void Process(Colony colony, ColonyProductionInfo production);

        protected float GetWorkerProductionModifier()
        {
            if (this.Workers == null || this.Workers.Count == 0)
            {
                return 0.0f;
            }

            int totalWorkerCount = this.Workers.Count;
            var traitsDistribution = new Dictionary<ColonistTrait, ushort>();

            foreach(var worker in this.Workers.Where(w => config.WorkingAge.InRange(w.Age)))
            {
                if (worker.Traits == null || worker.Traits.Length == 0)
                {
                    continue;
                }

                foreach (var workerTrait in worker.Traits)
                {
                    if (this.TraitsModifiers.ContainsKey(workerTrait))
                    {
                        ushort value = traitsDistribution.TryGetValue(workerTrait, out value) ? value : (ushort)0;
                        traitsDistribution[workerTrait] = (ushort)(value + 1);
                    }
                }
            }

            float modifier = 0.0f;
            foreach(var trait in traitsDistribution)
            {
                ushort coefficient = this.TraitsModifiers[trait.Key];
                modifier += trait.Value * coefficient / 1000.0f;
            }

            return modifier;
        } 
    }
}
