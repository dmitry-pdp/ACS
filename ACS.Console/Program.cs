using ACS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ColonistConfig
            {
                StartAge = 0,
                MaxAttributeValue = 10000,
                BaseAttributeDecay = 100
            };

            var colony = new Colony();
            colony.Colonists = new Colonist[100];
            for (int i = 0; i < 100; i++)
            {
                colony.Colonists[i] = new Colonist(config)
                {
                    Age = 0,
                    Energy = 100,
                    Health = 100,
                    Entertainment = 100,
                    Social = 100,
                    Hunger = 100,
                    State = ColonistState.Resting,
                    Traits = new ColonistTrait[]
                    {
                    }
                };
            }
        }
    }
}
