namespace ACS.Models.Test
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    [TestClass]
    public class PowerGeneratorColonyModuleTest
    {
        private PowerGeneratorColonyModule module;
        private ColonyConfig config;

        [TestInitialize]
        public void Setup()
        {
            this.config = new ColonyConfig();
            this.module = new PowerGeneratorColonyModule(this.config);

            this.config.WorkingAge = new RangeConstraint<ushort>(20, 40);
        }

        [TestMethod]
        public void PowerGeneratorColonyModule_Process_WithModifier_ReturnEnergyResource()
        {
            this.config.PowerGeneratorColonyModule_WorkerModifier = 100;
            this.module.Capacity = 20;
            this.module.PowerProduced = 50;

            this.module.Workers = new List<Colonist>
            {
                new Colonist(this.config)
                {
                    Age = 25,
                    Traits = null
                },
                new Colonist(this.config)
                {
                    Age = 31,
                    Traits = new ColonistTrait[]
                    {
                        ColonistTrait.Athletic
                    }
                }
            };

            this.module.TraitsModifiers = new Dictionary<ColonistTrait, ushort>
            {
                { ColonistTrait.Athletic, 5 }
            };

            var production = new ColonyProductionInfo();
            var colony = new Colony();

            this.module.Process(colony, production);

            var expectedProduction = 50 * (1.0f + 100 / 1000.0f * 2 / 20.0f + 1 * 5 / 1000.0f);

            Assert.AreEqual(production.Count, 1, "New resource should be added.");
            Assert.AreEqual(production[0].Module, this.module, "This module shall add a resource.");
            Assert.AreEqual(production[0].ResourceType, ResourceType.Energy, "Resource should be energy.");
            Assert.AreEqual(production[0].Amount, expectedProduction, 0.00001, "Amount of resources should be expected.");
        }
    }
}
