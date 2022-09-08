namespace ShadowrunTools.Characters.ViewModels.Traits
{
    using DynamicData.Binding;
    using ReactiveUI;
    using ShadowrunTools.Characters.Contract.Model;
    using ShadowrunTools.Characters.Traits;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Reflection;
    using System.Windows.Input;

    public class SkillViewModel : LeveledTraitViewModel, ISkillViewModel
    {
        private const int SpecializationBonus = 2;

        private readonly ISkill _skill;
        private static readonly ISet<string> _propertyNames;

        static SkillViewModel()
        {
            _propertyNames = new HashSet<string>(typeof(SkillViewModel)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !typeof(ICommand).IsAssignableFrom(p.PropertyType))
                .Select(pi => pi.Name));
        }

        public SkillViewModel(DisplaySettings displaySettings, ISkill model)
            : base(displaySettings, model)
        {
            _skill = model;
            _displayPool = this
                .WhenAnyValue(
                    x => x.AugmentedPool,
                    x => x.Specializations.Count,
                    (pool, spec) => spec > 0 ? $"{pool} ({pool + SpecializationBonus})" : $"{pool}")
                .ToProperty(this, x => x.DisplayPool)
                .DisposeWith(Disposables);

            Specializations.CollectionChanged += OnSpecializationsChanged;
        }

        #region ISkill Implementation

        public SkillType SkillType => _skill.SkillType;

        public string GroupName => _skill.GroupName;

        public IAttribute LinkedAttribute => _skill.LinkedAttribute;

        public bool AllowDefault => _skill.AllowDefault;

        public int TotalPool => _skill.TotalPool;

        public int AugmentedPool => _skill.AugmentedPool;

        public string UsualLimit => _skill.UsualLimit;

        private ObservableAsPropertyHelper<string> _displayPool;
        public string DisplayPool => _displayPool.Value;

        public IObservableCollection<string> Specializations => _skill.Specializations;

        public IReadOnlyCollection<string> SuggestedSpecializations => _skill.SuggestedSpecializations;

        #endregion

        private string m_DisplaySpecializations;
        public string DisplaySpecializations
        {
            get => m_DisplaySpecializations;
            set => this.RaiseAndSetIfChanged(ref m_DisplaySpecializations, value);
        }

        private void OnSpecializationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null || e.OldItems != null)
            {
                DisplaySpecializations = string.Join(", ", Specializations.ToArray());
            }
        }

        protected override void OnTraitChanged(string propertyName)
        {
            if (_propertyNames.Contains(propertyName))
            {
                this.RaisePropertyChanged(propertyName);
            }
        }
    }
}
