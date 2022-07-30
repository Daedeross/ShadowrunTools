using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ISkillsViewModel : IViewModel<ICharacter>
    {
        IObservableCollection<ISkillViewModel> Skills { get; }

        IObservableCollection<ISkillViewModel> ActiveSkills { get; }

        IObservableCollection<ISkillViewModel> KnowledgeSkills { get; }

        IObservableCollection<ISkillGroupViewModel> SkillGroups { get; }
    }
}
