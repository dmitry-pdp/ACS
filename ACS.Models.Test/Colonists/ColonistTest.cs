namespace ACS.Models.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using System;

    [TestClass]
    public class ColonistTest
    {
        private FakeRandomGenerator fakeRandomGenerator;

        [TestInitialize]
        public void Setup()
        {
            this.fakeRandomGenerator = new FakeRandomGenerator();
            EntropyGenerator.Initialize(this.fakeRandomGenerator);
        }

        [TestMethod]
        public void Colonist_Constructor_WithConstructionConfig_AgeAssigned()
        {
            byte startAge = (byte)(new Random().Next(100));
            var config = new ColonyConfig
            {
                StartAge = startAge
            };

            var colonist = new Colonist(config);
            Assert.AreEqual(startAge, colonist.Age, "Colonist age must be assigned from the config.StartAge.");
        }

        [TestMethod]
        public void Colonist_Constructor_WithConstructionConfig_AttributesInitialized()
        {
            ushort maxAttributeValue = (ushort)(new Random().Next(100));

            var config = new ColonyConfig
            {
                MaxAttributeValue = maxAttributeValue
            };

            var colonist = new Colonist(config);
            Assert.AreEqual(maxAttributeValue, colonist.Energy, "colonist.Energy is not assigned properly.");
            Assert.AreEqual(maxAttributeValue, colonist.Hunger, "colonist.Hunger is not assigned properly.");
            Assert.AreEqual(maxAttributeValue, colonist.Health, "colonist.Health is not assigned properly.");
            Assert.AreEqual(maxAttributeValue, colonist.Social, "colonist.Social is not assigned properly.");
            Assert.AreEqual(maxAttributeValue, colonist.Entertainment, "colonist.Entertainment is not assigned properly.");
        }

        [TestMethod]
        public void Colonist_Constructor_WithConstructionConfig_StateInitialized()
        {
            var colonist = new Colonist(new ColonyConfig());
            Assert.AreEqual(ColonistState.Resting, colonist.State, "Colonist must be in resting state once created.");
        }

        [TestMethod]
        public void Colonist_ProcessColonistState_WithDecayFromConfig_AttributesDecreased()
        {
            ushort maxAttributeValue = 1000;
            ushort baseAttributeDecay = (ushort)(new Random().Next(100));
            ushort baseAttributeDecayDeviation = (ushort)(new Random().Next(20));

            this.fakeRandomGenerator.nextValue = baseAttributeDecayDeviation;

            var colonist = new Colonist(new ColonyConfig
            {
                MaxAttributeValue = maxAttributeValue,
                BaseAttributeDecay = baseAttributeDecay,
                BaseAttributeDecayDeviation = baseAttributeDecayDeviation
            });

            colonist.ProcessColonistState();

            var attributeNewValue = maxAttributeValue - (baseAttributeDecay + (baseAttributeDecayDeviation / 2 - baseAttributeDecayDeviation));

            Assert.AreEqual(attributeNewValue, colonist.Energy, "colonist.Energy is not decayed properly.");
            Assert.AreEqual(attributeNewValue, colonist.Hunger, "colonist.Hunger is not decayed properly.");
            Assert.AreEqual(attributeNewValue, colonist.Health, "colonist.Health is not decayed properly.");
            Assert.AreEqual(attributeNewValue, colonist.Social, "colonist.Social is not decayed properly.");
            Assert.AreEqual(attributeNewValue, colonist.Entertainment, "colonist.Entertainment is not decayed properly.");
        }
    }
}
