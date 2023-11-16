namespace ShadowrunTools.Characters.ViewModels.Traits
{
    using ReactiveUI;
    using ShadowrunTools.Characters.Traits;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;

    public class SkillGroupViewModel : LeveledTraitViewModel, ISkillGroupViewModel
    {
        private readonly ISkillGroup _skillGroup;
        private static readonly ISet<string> _propertyNames;

        public IReadOnlyList<string> SkillNames => throw new System.NotImplementedException();

        public bool Broken => _skillGroup.Broken;

        static SkillGroupViewModel()
        {
            _propertyNames = new HashSet<string>(typeof(SkillViewModel)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !typeof(ICommand).IsAssignableFrom(p.PropertyType))
                .Select(pi => pi.Name));
        }

        public SkillGroupViewModel(DisplaySettings displaySettings, ISkillGroup model)
            : base(displaySettings, model)
        {
            _skillGroup = model;
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
