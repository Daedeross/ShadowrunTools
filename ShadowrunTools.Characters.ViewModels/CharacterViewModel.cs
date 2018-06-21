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
        private ICharacter _character;

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
