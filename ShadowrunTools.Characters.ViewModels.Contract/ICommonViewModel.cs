using DynamicData.Binding;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICommonViewModel : IViewModel<ICharacter>
    {
        IObservableCollection<IAttributeViewModel> Attributes { get; }

        IObservableCollection<IQualityViewModel> Qualities { get; }
    }
}
