using ShadowrunTools.Characters.ViewModels;
using System.Collections.Generic;

namespace ShadowrunTools.Characters
{
    public interface IViewModelFactory
    {
        T Create<T>() where T : class, IViewModel;

        TViewModel For<TViewModel, TModel>(TModel model) where TViewModel : class, IViewModel<TModel>;

        public void Release(IViewModel viewModel);
    }
}
