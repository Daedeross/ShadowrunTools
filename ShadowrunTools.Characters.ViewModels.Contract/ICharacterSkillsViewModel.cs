using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICharacterSkillsViewModel : IViewModel<ICharacter>
    {
        IObservableCollection<ISkillViewModel> Skills { get; }

        IObservableCollection<ISkillViewModel> ActiveSkills { get; }

        IObservableCollection<ISkillViewModel> KnowledgeSkills { get; }

        IObservableCollection<ISkillGroupViewModel> SkillGroups { get; }

        IReadOnlyCollection<string> ActiveSkillFilters { get; }

        IReadOnlyCollection<string> KnowledgeSkillFilters { get; }

        string SelectedActiveSkillFilter { get; set; }

        string ActiveSkillFilterText { get; set; }

        string KnowledgeFilterText { get; set; }

        string SelectedKnowledgeSkillFilter { get; set; }
    }
}
