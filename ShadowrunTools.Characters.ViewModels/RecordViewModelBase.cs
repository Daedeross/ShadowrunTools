using ReactiveUI;

namespace ShadowrunTools.Characters.ViewModels
{
    public abstract class RecordViewModelBase<T> : ViewModelBase
        where T : INotifyItemChanged
    {
        public T Record { get; private set; }

        public RecordViewModelBase(DisplaySettings displaySettings, T record)
            : base (displaySettings)
        {
            Record = record;
            record.ItemChanged += OnItemChanged;
        }

        private void OnItemChanged(object sender, ItemChangedEventArgs e)
        {
            foreach (var propertyName in e.PropertyNames)
            {
                this.RaisePropertyChanged(propertyName);
            }
        }

        #region IDisposable Support

        protected override void OnDispose(bool disposing)
        {
            base.OnDispose(disposing);
            Record.ItemChanged -= OnItemChanged;
        }

        #endregion
    }
}
