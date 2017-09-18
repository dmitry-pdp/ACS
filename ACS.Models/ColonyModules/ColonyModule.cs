using System;
namespace ACS.Model
{
    /* Colony modules are responsible for colony grows and resource production.
     * Every module has traits which benefits its yield if worker is assigned to this module.
     * Trait modifiers are in thousands of unit of production.
     */ 

    using System.Collections.Generic;

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

            for(int i = 0; i < totalWorkerCount; i++)
            {
                var worker = this.Workers[i];
                if (worker.Age > this.config.MaxWorkingAge || worker.Age < this.config.MinWorkingAge)
                {
                    continue;
                }

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
                ushort coefficient = this.TraitsModifiers.TryGetValue(trait.Key, out coefficient) ? coefficient : (ushort)0;
                modifier += trait.Value * coefficient / 1000.0f;
            }

            return modifier;
        } 
    }
}
