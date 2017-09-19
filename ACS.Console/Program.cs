namespace ACS.Console
{
    using ACS.Model;

    class Program
    {
        static void Main(string[] args)
        {
            var config = new ColonyConfig
            {
                StartAge = 0,
                MaxAttributeValue = 10000
            };

            var colony = new Colony();
            colony.Colonists = new Colonist[100];
            for (int i = 0; i < 100; i++)
            {
                colony.Colonists[i] = new Colonist(config);
            }
        }
    }
}
