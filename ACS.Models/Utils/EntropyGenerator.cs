namespace ACS.Model
{
    using System;

    public interface IRandomizer
    {
        int Next(int maxValue);
    }

    public class RandomizerImpl : IRandomizer
    {
        private static Random random = new Random();

        public int Next(int maxValue)
        {
            return random.Next(maxValue);
        }
    }

    public static class EntropyGenerator
    {
        private static IRandomizer random = new RandomizerImpl();

        public static void Initialize(IRandomizer randomizer)
        {
            EntropyGenerator.random = randomizer;
        }

        public static int Decay(int deviation)
        {
            return deviation / 2 - random.Next(deviation);
        }
    }
}
