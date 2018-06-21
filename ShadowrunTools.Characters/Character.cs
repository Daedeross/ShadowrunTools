namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Character: CategorizedTraitContainer, ICharacter
    {
        private readonly IRules _rules;

        public string Name { get; set; }

        public Character(IRules rules)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }
    }
}
