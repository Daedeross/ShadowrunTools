using ShadowrunTools.Characters.Traits;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ILeveledTraitViewModel : IViewModel, ILeveledTrait
    {
        string DisplayRating { get; }

        int MaxBaseIncrease { get; }

        int MaxImprovement { get; }
    }
}
