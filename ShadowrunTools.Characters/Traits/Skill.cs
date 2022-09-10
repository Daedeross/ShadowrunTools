namespace ShadowrunTools.Characters.Traits
{
    using DynamicData.Binding;
    using ReactiveUI;
    using ShadowrunTools.Characters.Contract.Model;
    using ShadowrunTools.Characters.Internal;
    using ShadowrunTools.Characters.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;

    public class Skill : LeveledTraitWithRequirements, ISkill
    {
        private readonly SkillGroup _skillGroup;
        private readonly IObservableCollection<Improvement> _improvements; // shared instance owned by the _skillGroup

        public Skill(
            Guid id,
            int prototypeHash,
            string name,
            string category,
            SkillType skillType,
            IAttribute linkedAttribuye,
            ISkillGroup skillGroup,
            IEnumerable<Improvement> improvements,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            IRules rules,
            IDslParser<ITrait> parser)
            : base(id, prototypeHash, name, category, container, root, rules, parser, LeveledTraitObservables.AugmentedRating)
        {
            SkillType = skillType;
            LinkedAttribute = linkedAttribuye;
            _skillGroup = (SkillGroup)skillGroup;

            _baseRating = this.WhenAnyValue(
                    me => me._skillGroup.BaseRating,
                    me => me.BaseIncrease,
                    me => me.Max,
                    (group, increase, max) => Math.Min(group + increase, max))
                .ToProperty(this, me => me.BaseRating)
                .DisposeWith(Disposables);

            _improvedRating = this.WhenAnyValue(
                    me => me._skillGroup.Improvement,
                    me => me.BaseRating,
                    me => me.Improvement,
                    me => me.Max,
                    (group, rating, improvement, max) => Math.Min(rating + improvement, max))
                .ToProperty(this, me => me.ImprovedRating)
                .DisposeWith(Disposables);

            _totalPool = this
                .WhenAnyValue(x => x.ImprovedRating, x => x.LinkedAttribute.ImprovedRating, x => x.AllowDefault, x => x.mRules.SkillDefaultAdjustment,
                    (skill_rat, attr_rat, allw_def, adj) => (skill_rat > 0 || allw_def) ? skill_rat + attr_rat + adj : 0)
                .ToProperty(this, x => x.TotalPool)
                .DisposeWith(Disposables);

            _augmentedPool = this
                .WhenAnyValue(x => x.AugmentedRating, x => x.LinkedAttribute.AugmentedRating, x => x.AllowDefault, x => x.mRules.SkillDefaultAdjustment,
                    (skill_rat, attr_rat, allw_def, adj) => (skill_rat > 0 || allw_def) ? skill_rat + attr_rat + adj : 0)
                .ToProperty(this, x => x.AugmentedPool)
                .DisposeWith(Disposables);

            _augmentedMax = this
                .WhenAnyValue(x => x.Max, max => (int)Math.Ceiling(max * 1.5))
                .ToProperty(this, x => x.AugmentedMax)
                .DisposeWith(Disposables);

            _improvements = AddToGroup(improvements);
        }

        #region Overrides

        public override int BaseIncrease
        {
            get => base.BaseIncrease;
            set
            {
                if (_skillGroup.BaseRating > 0)
                {
                    this.RaiseAndSetIfChanged(ref m_BaseIncrease, 0);
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref m_BaseIncrease, value);
                }
            }
        }

        private int _chargenImprovment;
        public override int Improvement
        {
            get => base.Improvement;
            set
            {
                var diff = value - Improvement;
                _skillGroup.ImproveSkill(this, )
            }
        }

        #endregion

        public SkillType SkillType { get; }

        private string m_GroupName;
        public string GroupName
        {
            get => m_GroupName;
            set => this.RaiseAndSetIfChanged(ref m_GroupName, value);
        }

        public ISkillGroup SkillGroup => _skillGroup;

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

        public override int Max => mRoot.InPlay ? mRules.InPlayMaxSkillRating : mRules.StartingMaxSkillRating;

        private readonly ObservableAsPropertyHelper<int> _augmentedMax;
        public override int AugmentedMax => _augmentedMax?.Value ?? Max;

        public override TraitType TraitType => TraitType.Skill;

        public override bool Independant { get; } = false;

        public override bool Improve(ImprovementSource source = ImprovementSource.Karma, int value = 1)
        {
            if (mRoot.InPlay)
            {
                if (ImprovedRating + value < Max)
                {
                    var oldValue = ImprovedRating;
                    int cost = 0;

                    Improvement++;

                    switch (source)
                    {
                        case ImprovementSource.Free:
                            break;
                        case ImprovementSource.Points:
                            cost = 1;
                            break;
                        case ImprovementSource.Karma:
                            cost = ImprovedRating * mRules.ActiveSkillKarmaMult;
                            break;
                        case ImprovementSource.Group:
                            break;
                        case ImprovementSource.Other:
                            break;
                        default:
                            break;
                    }

                    _skillGroup.ImproveSkill(this, new Improvement(Guid.NewGuid(), source, oldValue, ImprovedRating, cost));

                    return true;
                }
            }

            return false;
        }

        private IObservableCollection<Improvement> AddToGroup(IEnumerable<Improvement> improvements)
        {
            var sorted = improvements.OrderBy(i => i.NewValue).ToList();
            int newValue = 0;

            foreach (var improvement in sorted)
            {
                if (improvement.OldValue != newValue)
                {
                    throw new InvalidOperationException("Skill improvements do not line up.");
                }
                newValue = improvement.NewValue;
            }

            return _skillGroup.AddSkill(this, sorted);
        }
    }
}
