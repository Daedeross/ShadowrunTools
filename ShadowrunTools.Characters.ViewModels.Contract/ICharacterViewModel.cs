namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICharacterViewModel : IViewModel<ICharacter>, IDocumentViewModel
    {
        IPrioritiesViewModel Priorities { get; }

        ICommonViewModel Common { get; }

        ICharacterSkillsViewModel Skills { get; }
    }
}
