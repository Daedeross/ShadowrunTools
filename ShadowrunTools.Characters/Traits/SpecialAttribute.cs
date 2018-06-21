namespace ShadowrunTools.Characters.Traits
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SpecialAttribute : Attribute
    {
        public SpecialAttribute(Guid id, string name, ITraitContainer container, ICategorizedTraitContainer root, ICharacterMetatype metatype, IRules rules)
            : base(id, name, container, root, metatype, rules)
        {
        }
    }
}
