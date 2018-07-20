using GalaSoft.MvvmLight.Command;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShadowrunTools.Characters.ViewModels
{
    public class CharacterViewModel: NotificationObject
    {
        private readonly ICharacter _character;

        public CharacterViewModel(ICharacter character)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));

            var agility = _character[TraitCategories.Attribute]["Agility"];
            Agility = agility as IAttribute;
        }

        #region Character Properties

        public string Name { get => _character.Name; set => _character.Name = value; }

        #endregion // Character Properties

        #region Core Attributes

        public ILeveledTrait Agility { get; private set; }

        #endregion // Core Attributes

        #region Commands

        private ICommand mAddTraitCommand;

        public ICommand AddTraitCommand
        {
            get
            {
                if (mAddTraitCommand is null)
                {
                    mAddTraitCommand = new RelayCommand<ITraitPrototype>(AddTraitExecute);
                }
                return mAddTraitCommand;
            }
        }

        private void AddTraitExecute(ITraitPrototype prototype)
        {
            
        }

        #endregion // Commands
    }
}
