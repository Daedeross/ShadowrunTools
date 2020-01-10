using System.Collections.Generic;

namespace ShadowrunTools.Characters.Prototypes
{
    public interface IMetavariantPrototype
    {
        /// <summary>
        /// The name of the Metavariant. eg: Human, Nartaki, Cyclops, etc
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The "Primary Subspecies." eg: Human, Elf, Ork, etc.
        /// </summary>
        string Metatype { get; }

        /// <summary>
        /// The Min/Max attributes for the metatvariant.
        /// </summary>
        IReadOnlyCollection<IMetatypeAttribute> Attributes { get; }
    }
}
