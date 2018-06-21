namespace ShadowrunTools.Characters
{
    public class MetatypeAttribute : IMetatypeAttribute
    {
        public string Name { get; }

        public int Min { get; }

        public int Max { get; }

        public MetatypeAttribute(IMetatypeAttribute prototype)
        {
            Name = prototype.Name;
            Min = prototype.Min;
            Max = prototype.Max;
        }
    }
}
