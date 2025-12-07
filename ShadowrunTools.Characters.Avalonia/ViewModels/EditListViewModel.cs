namespace ShadowrunTools.Characters.Avalonia.ViewModels
{
    using ShadowrunTools.Foundation;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class EditListViewModel
    {
        public ObservableCollection<IProperty> Properties { get; set; }

        public EditListViewModel(IPropertyList properties)
        {
            Properties = new ObservableCollection<IProperty>(properties.Values);
        }
    }
}
