using ReactiveUI;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels.Traits
{
    public class QualityViewModel : LeveledTraitViewModel, IQualityViewModel
    {
        private readonly IQuality _quality;
        private static readonly ISet<string> _propertyNames;

        static QualityViewModel()
        {
            _propertyNames = new HashSet<string>(typeof(QualityViewModel)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !typeof(ICommand).IsAssignableFrom(p.PropertyType))
                .Select(pi => pi.Name));
        }

        public QualityViewModel(DisplaySettings displaySettings, IQuality model)
            : base(displaySettings, model)
        {
            _quality = model;
        }

        public bool HasRating => _quality.HasRating;

        protected override void OnTraitChanged(string propertyName)
        {
            if (_propertyNames.Contains(propertyName))
            {
                this.RaisePropertyChanged(propertyName);
            }
        }
    }
}
