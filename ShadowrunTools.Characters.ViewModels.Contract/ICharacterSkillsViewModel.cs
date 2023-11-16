using DynamicData.Binding;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICharacterSkillsViewModel : IViewModel<ICharacter>
    {
        IObservableCollection<ISkillGroupViewModel> SkillGroups { get; }

        IObservableCollection<ISkillViewModel> Skills { get; }

        IObservableCollection<ISkillViewModel> ActiveSkills { get; }

        IObservableCollection<ISkillViewModel> KnowledgeSkills { get; }

        IReadOnlyCollection<string> ActiveSkillFilters { get; }

        IReadOnlyCollection<string> KnowledgeSkillFilters { get; }

        string SelectedActiveSkillFilter { get; set; }

        string ActiveSkillSearchText { get; set; }

        string KnowledgeSkillSearchText { get; set; }

        string SelectedKnowledgeSkillFilter { get; set; }
    }
}
