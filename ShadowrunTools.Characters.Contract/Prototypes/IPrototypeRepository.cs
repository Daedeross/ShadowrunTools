namespace ShadowrunTools.Characters.Prototypes
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Characters.Priorities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IPrototypeRepository
    {
        /// <summary>
        /// Collection of available metavariants.
        /// </summary>
        IReadOnlyCollection<IMetavariantPrototype> Metavariants { get; }

        /// <summary>
        /// The default metatype that a character will be until a choice is made.
        /// </summary>
        IMetavariantPrototype DefaultMetavariant { get; }

        /// <summary>
        /// The priority table
        /// </summary>
        IPriorities Priorities { get; }

        ITraitPrototype GetTraitPrototype(TraitType traitType, string name);

        TPrototype GetTraitPrototype<TPrototype>(string name)
            where TPrototype : class, ITraitPrototype;

        IReadOnlyDictionary<string, TPrototype> GetTraits<TPrototype>()
            where TPrototype : class, ITraitPrototype;
    }
}
