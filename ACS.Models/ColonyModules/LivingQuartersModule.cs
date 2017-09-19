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
            var powerAvailable = colony.GetPower(this);

            foreach (var worker in this.GetWorkersWithinAge(this.config.BreedingAge))
            {
            }
        }
    }

}
