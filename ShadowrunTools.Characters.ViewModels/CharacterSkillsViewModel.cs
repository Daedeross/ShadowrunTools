using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.ViewModels.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class CharacterSkillsViewModel : ViewModelBase, ICharacterSkillsViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ICharacter _character;

        private readonly IObservableCache<ISkillViewModel, string> _skillsCache;
        private readonly IObservable<IChangeSet<ISkillViewModel, string>> _skillsChanges;

        public CharacterSkillsViewModel(DisplaySettings displaySettings, IViewModelFactory viewModelFactory, ICharacter model)
            : base(displaySettings)
        {
            _character = model ?? throw new ArgumentNullException(nameof(model));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _skillsCache = _character.Skills
                .ToViewModelCache<ISkillViewModel, ISkill>(_viewModelFactory);

            _skills = new ObservableCollectionExtended<ISkillViewModel>(
                _character.Skills.Values.Select(_viewModelFactory.For<ISkillViewModel, ISkill>));

            var activeFilter = this
                .WhenAnyValue(x => x.SelectedActiveSkillFilter, x => x.ActiveSkillSearchText)
                .Select(tuple => BuildActiveFilter(tuple.Item1, tuple.Item2));

            var knowledgeFilter = this
                .WhenAnyValue(x => x.SelectedKnowledgeSkillFilter, x => x.KnowledgeSkillSearchText)
                .Select(tuple => BuildKnowledgeFilter(tuple.Item1, tuple.Item2));

            _skillsChanges = _skillsCache.Connect();//.Publish();

            _skillsChanges
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(_skills)
                .Subscribe()
                .DisposeWith(Disposables);

            _activeSkills = new ObservableCollectionExtended<ISkillViewModel>(_skills.Where(SkillHelpers.IsActiveSkill).Cast<ISkillViewModel>());

            _skillsChanges
                .Filter(activeFilter)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(_activeSkills)
                .Subscribe()
                .DisposeWith(Disposables);

            _skillsChanges
                .Filter(knowledgeFilter)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(_knowledgeSkills)
                .Subscribe()
                .DisposeWith(Disposables);
        }

        private ObservableCollectionExtended<ISkillViewModel> _skills = new ObservableCollectionExtended<ISkillViewModel>();
        public IObservableCollection<ISkillViewModel> Skills => _skills;

        private ObservableCollectionExtended<ISkillViewModel> _activeSkills = new ObservableCollectionExtended<ISkillViewModel>();
        public IObservableCollection<ISkillViewModel> ActiveSkills => _activeSkills;

        private ObservableCollectionExtended<ISkillViewModel> _knowledgeSkills = new ObservableCollectionExtended<ISkillViewModel>();
        public IObservableCollection<ISkillViewModel> KnowledgeSkills => _knowledgeSkills;

        public IObservableCollection<ISkillGroupViewModel> SkillGroups { get; set; }

        #region Filtering

        private string m_ActiveSkillFilterText;
        public string ActiveSkillSearchText
        {
            get => m_ActiveSkillFilterText;
            set => this.RaiseAndSetIfChanged(ref m_ActiveSkillFilterText, value);
        }

        public IReadOnlyCollection<string> ActiveSkillFilters { get; } = SkillHelpers.ActiveSkillFilters.Keys.ToList();

        private string m_SelectedActiveSkillFilter;
        public string SelectedActiveSkillFilter
        {
            get => m_SelectedActiveSkillFilter;
            set => this.RaiseAndSetIfChanged(ref m_SelectedActiveSkillFilter, value);
        }

        private string m_KnowledgeFilterText;
        public string KnowledgeSkillSearchText
        {
            get => m_KnowledgeFilterText;
            set => this.RaiseAndSetIfChanged(ref m_KnowledgeFilterText, value);
        }

        public IReadOnlyCollection<string> KnowledgeSkillFilters { get; } = SkillHelpers.KnowledgeSkillFilters.Keys.ToList();

        private string m_SelectedKnowledgeSkillFilter;
        public string SelectedKnowledgeSkillFilter
        {
            get => m_SelectedKnowledgeSkillFilter;
            set => this.RaiseAndSetIfChanged(ref m_SelectedKnowledgeSkillFilter, value);
        }

        private Func<ISkill, bool> BuildActiveFilter(string selectedFilter, string searchText)
        {
            Predicate<ISkill> filterPredicate = null;
            Predicate<ISkill> searchPredicate = null;

            if (selectedFilter != null && SkillHelpers.ActiveSkillFilters.TryGetValue(selectedFilter, out var built_in))
            {
                filterPredicate = (skill) => built_in(skill);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchPredicate = (skill) => skill.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
            }
            if (filterPredicate is null)
            {
                if (searchPredicate is null)
                {
                    return SkillHelpers.IsActiveSkill;
                }
                else
                {
                    return (skill) => SkillHelpers.IsActiveSkill(skill) && searchPredicate(skill);
                }
            }
            else
            {
                if (searchPredicate is null)
                {
                    return (skill) => SkillHelpers.IsActiveSkill(skill) && filterPredicate(skill);
                }
                else
                {
                    return (skill) => SkillHelpers.IsActiveSkill(skill) && filterPredicate(skill) && searchPredicate(skill);
                }
            }
        }
        private Func<ISkill, bool> BuildKnowledgeFilter(string selectedFilter, string searchText)
        {
            var filter = SkillHelpers.IsKnowledgeSkill;

            if (selectedFilter != null && SkillHelpers.KnowledgeSkillFilters.TryGetValue(selectedFilter, out var built_in))
            {
                filter = (skill) => filter(skill) && built_in(skill);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filter = (skill) => filter(skill) && skill.Name.Contains(searchText);
            }

            return filter;
        }

        #endregion
    }
}
