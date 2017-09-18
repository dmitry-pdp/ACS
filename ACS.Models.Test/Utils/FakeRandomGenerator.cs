namespace ACS.Models.Test
{
    using ACS.Model;

    public class FakeRandomGenerator : IRandomizer
    {
        public int nextValue;

        public int Next(int maxValue)
        {
            return this.nextValue;
        }
    }
}
