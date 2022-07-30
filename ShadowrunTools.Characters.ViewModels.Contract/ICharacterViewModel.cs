using DynamicData.Binding;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICharacterViewModel : IViewModel<ICharacter>, IDocumentViewModel
    {
        IPrioritiesViewModel Priorities { get; }

        ICommonViewModel Common { get; }
    }
}
