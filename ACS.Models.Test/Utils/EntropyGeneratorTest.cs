namespace ACS.Models.Test.Utils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    [TestClass]
    public class EntropyGeneratorTest
    {
        [TestMethod]
        public void EntropyGenerator_Decay_WithDecayMax_ComputeDecayValue()
        {
            var fakeRandomGenerator = new FakeRandomGenerator();
            EntropyGenerator.Initialize(fakeRandomGenerator);

            fakeRandomGenerator.nextValue = 34;
            Assert.AreEqual((100 / 2 - 34), EntropyGenerator.Decay(100));

            fakeRandomGenerator.nextValue = 86;
            Assert.AreEqual((100 / 2 - 86), EntropyGenerator.Decay(100));
        }

        [TestMethod]
        public void EntropyGenerator_WithNextRandom_ComputeValidDecay()
        {
            EntropyGenerator.Initialize(new RandomizerImpl());
            var decay = EntropyGenerator.Decay(100);
            Assert.IsTrue(decay > -50, "Decay should be more than half of parameter.");
            Assert.IsTrue(decay < 50, "Decay should be less than half of parameter.");
        }
    }
}
