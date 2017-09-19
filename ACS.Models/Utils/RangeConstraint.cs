namespace ACS.Model
{
    using System;

    public struct RangeConstraint<T> where T : IComparable<T>
    {
        public T MinValue;
        public T MaxValue;

        public RangeConstraint(T minValue, T maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public bool InRange(T value)
        {
            return this.MinValue.CompareTo(value) <= 0 && this.MaxValue.CompareTo(value) >= 0;
        }
    }
}
