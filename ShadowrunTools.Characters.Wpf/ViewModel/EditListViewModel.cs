namespace ShadowrunTools.Characters.Wpf.ViewModel
{
    using ShadowrunTools.Foundation;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class EditListViewModel
    {
        private readonly IDictionary<string, IProperty> dictionary;
        public ObservableCollection<IProperty> Properties { get; set; }

        public EditListViewModel(IPropertyList properties)
        {
            dictionary = properties;
            Properties = new ObservableCollection<IProperty>(properties.Values);
        }
    }
}
