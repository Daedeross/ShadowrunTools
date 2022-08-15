using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class CommonViewModel : ViewModelBase, ICommonViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly ICharacter _character;

        public CommonViewModel(DisplaySettings displaySettings, IViewModelFactory viewModelFactory, ICharacter model)
            : base(displaySettings)
        {
            _character = model ?? throw new ArgumentNullException(nameof(model));
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));

            _attributes = new ObservableCollectionExtended<IAttributeViewModel>(
                _character.Attributes.Values.Select(_viewModelFactory.For<IAttributeViewModel, IAttribute>));

            _character.Attributes
                .ToViewModelCollection<IAttributeViewModel, IAttribute>(_viewModelFactory)
                .Sort(SortExpressionComparer<IAttributeViewModel>.Ascending(vm => vm.CustomOrder).ThenByAscending(vm => vm.Name))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(_attributes)
                .Subscribe(OnNext)
                .DisposeWith(Disposables);

            _character.Qualities
                .ToViewModelCollection<IQualityViewModel, IQuality>(_viewModelFactory)
                .Sort(SortExpressionComparer<IQualityViewModel>.Ascending(vm => vm.Name))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(_qualities)
                .Subscribe()
                .DisposeWith(Disposables);
        }

        private IObservableCollection<IAttributeViewModel> _attributes;
        public IObservableCollection<IAttributeViewModel> Attributes => _attributes;

        private IObservableCollection<IQualityViewModel> _qualities = new ObservableCollectionExtended<IQualityViewModel>();
        public IObservableCollection<IQualityViewModel> Qualities => _qualities;

        private void OnNext(IChangeSet<IAttributeViewModel> changes)
        {
            ;
        }
    }
}
