namespace ShadowrunTools.Characters.Traits
{
    using DynamicData.Binding;
    using ReactiveUI;
    using ShadowrunTools.Characters;
    using ShadowrunTools.Characters.Internal;
    using ShadowrunTools.Characters.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reactive.Disposables;

    public class SkillGroup : LeveledTraitWithRequirements, ISkillGroup
    {
        private readonly Dictionary<ISkill, ObservableCollectionExtended<Improvement>> _skills;

        public SkillGroup(
            Guid id,
            int prototypeHash,
            string name,
            string category,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            IList<ISkill> skills,
            IRules rules,
            IDslParser<ITrait> parser)
            : base(id, prototypeHash, name, category, container, root, rules, parser)
        {
            _skills = new ();

            _augmentedMax = this
                .WhenAnyValue(x => x.Max, max => (int)Math.Ceiling(max * 1.5))
                .ToProperty(this, x => x.AugmentedMax)
                .DisposeWith(Disposables);
        }

        public IReadOnlyList<string> SkillNames { get; }

        private bool m_Broken = false;
        public bool Broken
        {
            get => m_Broken;
            protected set => this.RaiseAndSetIfChanged(ref m_Broken, value);
        }

        public override int Min => 0;

        public override int Max => mRoot.InPlay ? mRules.InPlayMaxSkillRating : mRules.StartingMaxSkillRating;

        private readonly ObservableAsPropertyHelper<int> _augmentedMax;
        public override int AugmentedMax => _augmentedMax?.Value ?? Max;

        public override TraitType TraitType => TraitType.SkillGroup;

        public override bool Independant => false;

        #region Overridden

        public override int BaseIncrease
        {
            get => base.BaseIncrease;
            set
            {
                if (CanChange(ImprovementSource.Points) && value > 0)
                {
                    this.RaiseAndSetIfChanged(ref m_BaseIncrease, value);
                }
                else
                {
                    this.RaisePropertyChanged();
                }
            }
        }

        private int _chargenImprovment;
        public override int Improvement
        {
            get => m_Improvement + _chargenImprovment;
            set
            {
                if (CanChange(ImprovementSource.Karma) && value > 0)
                {
                    if (mRoot.InPlay)
                    {
                        var diff = value - ImprovedRating;

                        while (diff-- > 0)
                        {
                            Improve();
                        }
                    }
                    else
                    {
                        this.RaiseAndSetIfChanged(ref _chargenImprovment, value);
                    }
                }
                else
                {
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        internal Improvement ImproveSkill(ISkill skill, int amount)
        {
            if (amount == 0)
            {
                return null;
            }
            else if (amount > 0)
            {
                for (int i = 1; i < amount; i++)
                {
                }
            }
            else
            {

            }

            RecalcBroken();
            RecalcBonus();
        }

        public override bool Improve(ImprovementSource source = ImprovementSource.Karma, int value = 1)
        {
            if (Broken || source == ImprovementSource.Points)
            {
                return false;
            }

            var newRating = ImprovedRating + value;
            if (newRating < Max && AugmentedRating + value < AugmentedMax)
            {
                PushGroupImprovement(new Improvement(Guid.NewGuid(), ImprovementSource.Karma, ImprovedRating, newRating, mRules.SkillGroupKarma(ImprovedRating, newRating)));
                m_Improvement++;
                this.RaisePropertyChanged(nameof(Improvement));
            }
            return true;
        }

        private void PushGroupImprovement(Improvement improvement)
        {
            if (Broken || !(_skills.Values.Select(s => s.LastOrDefault()?.NewValue ?? 0).Distinct().Count() == 1)) // Sanity check
            {
                throw new InvalidOperationException();
            }

            foreach (var kvp in _skills)
            {
                kvp.Value.Add(improvement);
                kvp.Key.Improve(ImprovementSource.Group);
            }
        }

        internal IObservableCollection<Improvement> AddSkill(ISkill skill, IEnumerable<Improvement> improvements)
        {
            var output = new ObservableCollectionExtended<Improvement>(improvements);
            _skills.Add(skill, output);
            skill.PropertyChanged += OnSkillChanged;

            return output;
        }

        private void OnSkillChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISkill.ImprovedRating) && _skills.Count > 1)
            {
                RecalcBroken();
                RecalcImprovement();
            }
        }

        private void RecalcBroken()
        {
            var ratings = _skills.Keys.Select(s => s.ImprovedRating).Distinct().ToList();
            if (ratings.Count == 1)
            {
                Broken = false;
            }
            else
            {
                Broken = true;
            }
        }

        private void RecalcImprovement()
        {
            if (mRoot.InPlay)
            {
                var rating = _skills.Keys.Min(s => s.Improvements.Max(i => i.NewValue));
                var in_play = rating - _chargenImprovment - BaseRating;
                this.RaiseAndSetIfChanged(ref m_Improvement, in_play, nameof(Improvement));
            }
        }

        private bool CanChange(ImprovementSource source)
        {
            if (mRoot.InPlay)
            {
                if (source == ImprovementSource.Karma)
                {
                    return true;
                }
                else
                {
                    throw new NotSupportedException("Can only improve SkillGroups in-play with karma.");
                }
            }
            else
            {
                if (source == ImprovementSource.Points)
                {
                    // Cannot break skill groups with points (SR5 p88)
                    return _skills.Keys.All(s => s.BaseIncrease == 0);
                }
                else if (source == ImprovementSource.Karma)
                {
                    return !Broken;
                }
                else
                {
                    throw new NotSupportedException("Can only improve SkillGroups with points or karma.");
                }
            }
        }
    }
}
