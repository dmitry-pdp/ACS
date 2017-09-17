namespace ACS.Models.Models
{
    using System;

    public static class EntropyGenerator
    {
        private static Random random = new Random();

        public static int Decay(int deviation)
        {
            return deviation / 2 - random.Next(deviation);
        }
    }
}
