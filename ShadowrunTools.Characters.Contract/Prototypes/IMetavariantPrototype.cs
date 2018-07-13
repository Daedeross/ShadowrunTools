using System.Collections.Generic;

namespace ShadowrunTools.Characters.Prototypes
{
    public interface IMetavariantPrototype
    {
        /// <summary>
        /// The name of the Metavariant. eg: Human, Nartaki, Cyclops, etc
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The "Primary Subspecies." eg: Human, Elf, Ork, etc.
        /// </summary>
        string Metatype { get; set; }
        List<IMetatypeAttribute> Attributes { get; set; }
    }
}
