namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Character: CategorizedTraitContainer, ICharacter
    {
        public string Name { get; set; }
    }
}
