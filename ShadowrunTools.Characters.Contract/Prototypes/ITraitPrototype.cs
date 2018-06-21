using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.Prototypes
{
    public interface ITraitPrototype
    {
        TraitType TraitType { get; }

        string Name { get; }

        string Category { get; }

        string SubCategory { get; }

        string UserNotes { get; }

        string Book { get; }

        int Page { get; }
    }
}
