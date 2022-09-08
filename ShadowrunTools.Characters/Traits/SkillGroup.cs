namespace ShadowrunTools.Characters.Traits
{
    using ShadowrunTools.Characters;
    using ShadowrunTools.Characters.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SkillGroup : LeveledTrait, ISkillGroup
    {
        public SkillGroup(
            Guid id,
            int prototypeHash,
            string name,
            string category,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            IRules rules)
            : base(id, prototypeHash, name, category, container, root, rules)
        {

        }

        public IEnumerable<string> Skills { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Broken => throw new NotImplementedException();

        public override int Min => throw new NotImplementedException();

        public override int Max => throw new NotImplementedException();

        public override int AugmentedMax => throw new NotImplementedException();

        public override TraitType TraitType => throw new NotImplementedException();

        public override bool Independant => throw new NotImplementedException();
    }
}
