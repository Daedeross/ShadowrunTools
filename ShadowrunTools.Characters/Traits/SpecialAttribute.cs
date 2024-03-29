﻿namespace ShadowrunTools.Characters.Traits
{
    using ShadowrunTools.Characters.Model;
    using System;

    public class SpecialAttribute : Attribute
    {
        public SpecialAttribute(Guid id, string name, ITraitContainer container, ICategorizedTraitContainer root, ICharacterMetatype metatype, IRules rules)
            : base(id, 0, name, container, root, metatype, rules)
        {

        }

        public override TraitType TraitType => TraitType.SpecialAttribute;

        public override bool Independant => false;
    }
}
