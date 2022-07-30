using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.ViewModels;
using System.Collections.Generic;

namespace ShadowrunTools.Characters
{
    public interface IViewModelFactory
    {
        T Create<T>() where T : class, IViewModel;

        TViewModel For<TViewModel, TModel>(TModel model) where TViewModel : class, IViewModel<TModel>;

        ICharacterViewModel Character(ICharacter character, IPriorities priorities);

        IViewContainer CreateContainer(string title, IViewModel content, bool ownsContent);

        void Release(object viewModel);
    }
}
