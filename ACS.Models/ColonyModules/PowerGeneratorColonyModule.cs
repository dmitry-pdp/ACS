namespace ACS.Model
{
    /* Power generators produce power and pollution.
     * Also have workers assigned to them.
     */

    using System.Linq;

    public class PowerGeneratorColonyModule : ColonyModule
    {
        public ushort PowerProduced;

        public PowerGeneratorColonyModule(ColonyConfig config) : base(config)
        {
        }

        /* Produces power, worker assigned gets extra decays.
         * Power produced is equal to nominal power multiplied by number of workers assigned with extra traits.
         * Eg: nominal 100MW, Workers: 5/10, Worker with traits: 2. TotalPower = 100 * (1 + <modifier> * 5/10 + <traits_modifier> * 2);
         *     <modifier> = 20%, <traits_modifier> = 0.5%, 
         *     then total ouput = 100 * (1 + 0.5 * 0.2 + 0.005 * 2) = 100 * 1.11 (11% increase of 0.2 * 1.0 + 0.005 * 10 = 25%)
         */
        public override void Process(Colony colony, ColonyProductionInfo production)
        {
            // Produce energy

            var workerCount = this.GetWorkersWithinAge(this.config.WorkingAge).Count();
            var workerModifier = this.config.PowerGeneratorColonyModule_WorkerModifier * workerCount / (this.Capacity * 1000.0f);
            var traitsModifier = this.GetWorkerProductionModifier();
            var powerProduced =  this.PowerProduced * (1.0f + workerModifier + traitsModifier);
            production.Add(this, ResourceType.Energy, powerProduced);

            // Decay workers attributes

            this.Workers.ForEach(worker => worker.DecayAttributes(this.WorkerAttributeDecayData));
        }
    }
}
