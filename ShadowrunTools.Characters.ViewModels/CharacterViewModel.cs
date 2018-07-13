using GalaSoft.MvvmLight.Command;
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
        }

        #region Commands

        private ICommand mAddTraitCommand;

        public ICommand AddTraitCommand
        {
            get
            {
                if (mAddTraitCommand is null)
                {
                    mAddTraitCommand = new RelayCommand<TraitPrototypeBase>(AddTraitExecute);
                }
                return mAddTraitCommand;
            }
        }

        private void AddTraitExecute(TraitPrototypeBase prototype)
        {
            
        }

        #endregion // Commands
    }
}
