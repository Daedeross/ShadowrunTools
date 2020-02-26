namespace ShadowrunTools.Characters.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    public class CharacterStatusViewModel : ViewModelBase
    {
        private readonly ICharacter _character;

        public CharacterStatusViewModel(DisplaySettings displaySettings, ICharacter character)
            : base(displaySettings)
        {
            _character = character;
        }

        public ObservableCollection<StatusItemViewModel> Items { get; set; }
    }
}
