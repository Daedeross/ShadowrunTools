﻿namespace ShadowrunTools.Characters.Prototypes
{
    using ShadowrunTools.Characters.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public interface IPrototypeRepository
    {
        List<IMetavariantPrototype> Metavariants { get; }

        ITraitPrototype GetTraitPrototype(TraitType traitType, string name);

        TPrototype GetTraitPrototype<TPrototype>(string name)
            where TPrototype : class, ITraitPrototype;
    }
}
