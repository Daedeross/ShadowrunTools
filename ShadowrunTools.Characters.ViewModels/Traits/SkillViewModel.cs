using DynamicData.Binding;
using ReactiveUI;
using ShadowrunTools.Characters.Contract.Model;
using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels.Traits
{
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

        protected override void OnTraitChanged(string propertyName)
        {
            if (_propertyNames.Contains(propertyName))
            {
                this.RaisePropertyChanged(propertyName);
            }
        }
    }
}
