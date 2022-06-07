using ShadowrunTools.Characters.ViewModels;
using System.Collections.Generic;

namespace ShadowrunTools.Characters
{
    public interface IViewModelFactory
    {
        T Create<T>() where T : class, IViewModel;

        public void Release(IViewModel viewModel);
    }
}
