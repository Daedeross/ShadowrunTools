using DynamicData.Binding;
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
                .WhenAnyValue(x => x.ImprovedRating, x => x.LinkedAttribute.ImprovedRating, x => x.AllowDefault, (skill_rat, attr_rat, alw_def) => (skill_rat > 0 || alw_def) ? skill_rat + attr_rat : 0)
                .ToProperty(this, x => x.TotalPool)
                .DisposeWith(Disposables);

            _augmentedPool = this
                .WhenAnyValue(x => x.AugmentedRating, x => x.LinkedAttribute.AugmentedRating, x => x.AllowDefault, (skill_rat, attr_rat, alw_def) => (skill_rat > 0 || alw_def) ? skill_rat + attr_rat : 0)
                .ToProperty(this, x => x.AugmentedPool)
                .DisposeWith(Disposables);

            _augmentedMax = this
                .WhenAnyValue(x => x.Max, max => (int)Math.Floor(max * 1.5))
                .ToProperty(this, x => x.AugmentedMax)
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

        private bool m_AllowDefault;
        public bool AllowDefault
        {
            get => m_AllowDefault;
            set => this.RaiseAndSetIfChanged(ref m_AllowDefault, value);
        }

        private readonly ObservableAsPropertyHelper<int> _totalPool;
        public int TotalPool => _totalPool.Value;

        private readonly ObservableAsPropertyHelper<int> _augmentedPool;
        public int AugmentedPool => _augmentedPool.Value;

        public string UsualLimit { get; set; }

        public IObservableCollection<string> Specializations { get; } = new ObservableCollectionExtended<string>();

        public IReadOnlyCollection<string> SuggestedSpecializations { get; set; }

        public override int Min => 0;

        public override int Max => mRules.StartingMaxSkillRating;

        private readonly ObservableAsPropertyHelper<int> _augmentedMax;
        public override int AugmentedMax => _augmentedMax?.Value ?? Max;

        public override TraitType TraitType => TraitType.Skill;

        public override bool Independant { get; } = false;
    }
}
