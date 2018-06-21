namespace ShadowrunTools.Characters.Prototypes
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
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
