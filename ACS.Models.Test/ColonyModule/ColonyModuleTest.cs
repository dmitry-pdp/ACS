namespace ACS.Models.Test
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    [TestClass]
    public class ColonyModuleTest
    {
        private class ColonyModuleImp : ColonyModule
        {
            public ColonyModuleImp(ColonyConfig config) : base(config)
            {
            }

            public ColonyConfig GetConfig()
            {
                return this.config;
            }

            public float GetWorkerProductionModifierExternal()
            {
                return this.GetWorkerProductionModifier();
            }

            public override void Process(Colony colony, ColonyProductionInfo production)
            {
                throw new NotImplementedException();
            }
        }

        private ColonyConfig config;
        private ColonyModuleImp colonyModule;

        [TestInitialize]
        public void Setup()
        {
            this.config = new ColonyConfig();
            this.colonyModule = new ColonyModuleImp(config);
        }

        [TestMethod]
        public void ColonyModule_Constructor_WithConfigProvided_AssignConfig()
        {
            Assert.AreSame(this.config, this.colonyModule.GetConfig());
        }

        [TestMethod]
        public void ColonyModule_GetWorkerProductionModifier__WithNoWorkers_ReturnZero()
        {
            this.config.WorkingAge = new RangeConstraint<ushort>(10, 40);

            this.colonyModule.TraitsModifiers = new Dictionary<ColonistTrait, ushort>
            {
                { ColonistTrait.Athletic, (ushort)100 },
                { ColonistTrait.Geek, (ushort)10 }
            };

            Assert.AreEqual(0.0, this.colonyModule.GetWorkerProductionModifierExternal(), 0.0001, "Production modifier is not calculated correctly.");
        }

        [TestMethod]
        public void ColonyModule_GetWorkerProductionModifier_WithTraitsAndWorders_ComputeModifier()
        {
            this.config.WorkingAge = new RangeConstraint<ushort>(10, 40);

            this.colonyModule.Workers = new List<Colonist>
            {
                new Colonist(this.config)
                {
                    Age = 50,
                    Traits = new[] { ColonistTrait.Athletic, ColonistTrait.Geek }
                },
                new Colonist(this.config)
                {
                    Age = 30,
                    Traits = new[] { ColonistTrait.Athletic, ColonistTrait.Geek }
                },
                new Colonist(this.config)
                {
                    Age = 10,
                    Traits = new[] { ColonistTrait.Geek }
                },
                new Colonist(this.config)
                {
                    Age = 0,
                    Traits = new[] { ColonistTrait.Athletic }
                },
                new Colonist(this.config)
                {
                    Age = 20,
                    Traits = null
                },
                new Colonist(this.config)
                {
                    Age = 25,
                    Traits = new[] { ColonistTrait.Nothing }
                }
            };

            this.colonyModule.TraitsModifiers = new Dictionary<ColonistTrait, ushort>
            {
                { ColonistTrait.Athletic, (ushort)100 },
                { ColonistTrait.Geek, (ushort)10 }
            };

            float expected = ((100 + 10) + 10) / 1000.0f;
            Assert.AreEqual(expected, this.colonyModule.GetWorkerProductionModifierExternal(), 0.0001, "Production modifier is not calculated correctly.");
        }
    }
}
