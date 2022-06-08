using DynamicData.Binding;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICharacterViewModel : IDocumentViewModel, IViewModel<ICharacter>
    {
        string Name { get; set; }

        IObservableCollection<IAttributeViewModel> Attributes { get; }

        ILeveledTrait Body { get; }
        ILeveledTrait Agility { get; }
        ILeveledTrait Reaction { get; }
        ILeveledTrait Strength { get; }

        ILeveledTrait Willpower { get; }
        ILeveledTrait Logic { get; }
        ILeveledTrait Intuition { get; }
        ILeveledTrait Charisma { get; }
    }
}
