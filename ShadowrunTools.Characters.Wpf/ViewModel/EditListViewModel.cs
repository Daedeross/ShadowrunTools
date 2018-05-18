namespace ShadowrunTools.Characters.Wpf.ViewModel
{
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class EditListViewModel
    {
        private IDictionary<string, IProperty> dictionary;
        public ObservableCollection<IProperty> Properties { get; set; }

        public EditListViewModel(IPropertyList properties)
        {
            dictionary = properties;
            Properties = new ObservableCollection<IProperty>(properties.Values);
        }
    }
}
