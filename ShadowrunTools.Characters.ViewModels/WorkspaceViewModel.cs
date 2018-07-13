namespace ShadowrunTools.Characters.ViewModels
{
    using ShadowrunTools.Characters.Prototypes;
    using ShadowrunTools.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    
    public class WorkspaceViewModel: NotificationObject
    {
        private IPrototypeRepository _prototypes;
        private readonly DataLoader _dataLoader;

        public IPrototypeRepository Prototypes
        {
            get
            {
                if (_prototypes == null)
                {
                    _prototypes = _dataLoader.ReloadAll();
                }
                return _prototypes;
            }
        }

        public ObservableCollection<CharacterViewModel> Characters { get; set; }

        public WorkspaceViewModel(DataLoader dataLoader)
        {
            _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
        }
    }
}
