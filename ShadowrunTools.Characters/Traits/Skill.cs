using ReactiveUI;
using ShadowrunTools.Characters.Contract.Model;
using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace ShadowrunTools.Characters.Traits
{
    public class Skill : LeveledTrait, ISkill
    {
        public Skill(
            Guid id,
            int prototypeHash,
            string name,
            string category,
            SkillType skillType,
            IAttribute linkedAttribuye,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            IRules rules)
            : base(id, prototypeHash, name, category, container, root, rules)
        {
            SkillType = skillType;
            LinkedAttribute = linkedAttribuye;

            _totalPool = this
                .WhenAnyValue(x => x.ImprovedRating, x => x.LinkedAttribute.ImprovedRating, (sk, at) => sk + at)
                .ToProperty(this, x => x.TotalPool)
                .DisposeWith(Disposables);

            _augmentedPool = this
                .WhenAnyValue(x => x.AugmentedRating, x => x.LinkedAttribute.AugmentedRating, (sk, at) => sk + at)
                .ToProperty(this, x => x.AugmentedPool)
                .DisposeWith(Disposables);
        }

        public SkillType SkillType { get; }

        private string m_GroupName;
        public string GroupName
        {
            get => m_GroupName;
            set => this.RaiseAndSetIfChanged(ref m_GroupName, value);
        }

        public IAttribute LinkedAttribute { get; }

        private ObservableAsPropertyHelper<int> _totalPool;
        public int TotalPool => _totalPool.Value;

        private ObservableAsPropertyHelper<int> _augmentedPool;
        public int AugmentedPool => _augmentedPool.Value;

        public string UsualLimit => throw new NotImplementedException();

        public IList<string> Specializations => throw new NotImplementedException();

        public override int Min => 1;

        public override int Max => mRules.StartingMaxSkillRating;

        public override int AugmentedMax => throw new NotImplementedException();

        public override TraitType TraitType => throw new NotImplementedException();

        public override bool Independant => throw new NotImplementedException();
    }
}
