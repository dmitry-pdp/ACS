namespace ACS.Model
{
    /* Living quarters module is where workers are resting and breeding.
     * Only people 
     */

    using System.Linq;

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

            foreach (var worker in this.Workers.Where(w => this.config.BreedingAge.InRange(w.Age)))
            {
            }
        }
    }

}
