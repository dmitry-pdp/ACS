namespace ACS.Model
{
    /* Living quarters module is where workers rest, eat and breed.
     * It's modifier depends on the power and pollution.
     */

    using System.Linq;

    public class LivingQuartersModule : ColonyModule
    {
        public ushort BonusFertility;
        public ushort BonusRest;
        public ushort PowerRequired;

        public LivingQuartersModule(ColonyConfig config) : base(config)
        {
        }

        public override void Process(Colony colony, ColonyProductionInfo production)
        {
            var powerAvailable = (float)colony.GetPower(this);
            var powerEffeciencyModifier = powerAvailable / this.PowerRequired;

            // Decay attributes for all colonists

            this.Workers.ForEach(colonist => colonist.DecayAttributes(this.WorkerAttributeDecayData));

            // Breed colonists

            foreach (var colonist in this.GetWorkersWithinAge(this.config.BreedingAge))
            {

            }
        }
    }

}
