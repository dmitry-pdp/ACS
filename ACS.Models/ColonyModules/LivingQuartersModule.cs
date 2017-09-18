namespace ACS.Model
{
    /* Living quarters module is where workers are resting and breeding.
     * Only people 
     */

    public class LivingQuartersModule : ColonyModule
    {
        public ushort BonusFertility;
        public ushort BonusRest;

        public LivingQuartersModule(ColonyConfig config) : base(config)
        {
        }

        public override void Process(Colony colony, ColonyProductionInfo production)
        {
            if (this.Workers == null || this.Workers.Count == 0)
            {
                return;
            }

            foreach (var worker in this.Workers)
            {
                if (worker.Age > this.config.MaxBreedingAge || worker.Age < this.config.MinBreedingAge)
                {
                    continue;
                }


            }
        }
    }

}
