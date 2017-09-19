namespace ACS.Model
{
    public enum ColonistAttributeType : byte
    {
        Energy  = 1,
        Hunger  = 2,
        Health  = 3,
        Social  = 4,
        Entertainment = 5
    }

    public struct ColonistAttribute
    {
        public ColonistAttributeType Type;
        public ushort Value;
        public ushort MaxValue;

        public ColonistAttribute(ColonistAttributeType type, ushort maxValue, ushort value = 0)
        {
            this.Type = type;
            this.MaxValue = maxValue;
            this.Value = value;
        }
    }
}
