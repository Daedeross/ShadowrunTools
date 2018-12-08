﻿using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Characters.ViewModels.Traits
{
    public class AttributeViewModel : LeveledTraitViewModel, IAttribute
    {
        private readonly IAttribute _attribute;
        private static readonly ISet<string> _propertyNames;

        static AttributeViewModel()
        {
            _propertyNames = new HashSet<string>(typeof(AttributeViewModel).GetProperties(System.Reflection.BindingFlags.Instance).Select(pi => pi.Name));
        }

        public AttributeViewModel(IAttribute attribute)
            : base(attribute)
        {
            _attribute = attribute;
        }

        #region IAttribute

        public string ShortName => _attribute.ShortName;

        public string CustomOrder { get => _attribute.CustomOrder; set => _attribute.CustomOrder = value; }

        #endregion

        protected override void OnTraitChanged(object sender, ItemChangedEventArgs e)
        {
            foreach (var propName in e.PropertyNames)
            {
                if (_propertyNames.Contains(propName))
                {
                    RaisePropertyChanged(propName);
                }
            }
        }
    }
}