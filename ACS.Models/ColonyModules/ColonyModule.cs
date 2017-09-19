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
        public ushort ColonistModifier;
        public List<Colonist> Workers;
        public Dictionary<ColonistTrait, ushort> TraitsModifiers;
        public WorkerAttributeDecay WorkerAttributeDecayData;

        protected ColonyConfig config;

        public ColonyModule(ColonyConfig config)
        {
            this.config = config;
        }

        public abstract void Process(Colony colony, ColonyProductionInfo production);

        protected IEnumerable<Colonist> GetWorkersWithinAge(RangeConstraint<ushort> constraint)
        {
            if (this.Workers == null || this.Workers.Count == 0)
            {
                return Enumerable.Empty<Colonist>();
            }

            return this.Workers.Where(w => constraint.InRange(w.Age));
        }

        /* Triat modifier is computed as an extra modification based on the number of colonists with a given trait.
         * Number is absolute to population assigned to the module.
         */
        protected float GetWorkerProductionModifier()
        {
            if (this.Workers == null || this.Workers.Count == 0)
            {
                return 0.0f;
            }

            int totalWorkerCount = this.Workers.Count;
            var traitsDistribution = new Dictionary<ColonistTrait, ushort>();

            foreach(var worker in this.GetWorkersWithinAge(config.WorkingAge))
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
