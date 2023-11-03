using ReactiveUI;
using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels.Traits
{
    public class AttributeViewModel : LeveledTraitViewModel, IAttributeViewModel
    {
        private readonly IAttribute _attribute;
        private static readonly ISet<string> _propertyNames;

        static AttributeViewModel()
        {
            _propertyNames = new HashSet<string>(typeof(AttributeViewModel)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => ! typeof(ICommand).IsAssignableFrom(p.PropertyType))
                .Select(pi => pi.Name));
        }

        public AttributeViewModel(DisplaySettings displaySettings, IAttribute model)
            : base(displaySettings, model)
        {
            _attribute = model;
        }

        #region IAttribute

        public string ShortName => _attribute.ShortName;

        public string CustomOrder { get => _attribute.CustomOrder; set => _attribute.CustomOrder = value; }

        public int Points => _attribute.Points;

        public int Karma => _attribute.Karma;

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
