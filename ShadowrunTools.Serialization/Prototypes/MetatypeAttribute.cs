using ShadowrunTools.Characters;

namespace ShadowrunTools.Serialization.Prototypes
{
    public class MetatypeAttributePrototype: IMetatypeAttribute
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
