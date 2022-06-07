using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IDisposable
    {
        protected CompositeDisposable Disposables { get; } = new CompositeDisposable();

        protected DisplaySettings _displaySettings { get; private set; }

        public ViewModelBase(DisplaySettings displaySettings)
        {
            _displaySettings = displaySettings;

            _displaySettings.PropertyChanged += this.DisplaySettingsPropertyChanged;
        }

        protected virtual void OnDisplaySettingsPropertyChanged(string propertyName)
        {

        }

        private void DisplaySettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnDisplaySettingsPropertyChanged(e.PropertyName);
        }

        protected virtual void OnDispose(bool disposing)
        {
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                OnDispose(disposing);
                if (disposing)
                {
                    _displaySettings.PropertyChanged -= DisplaySettingsPropertyChanged;
                    Disposables.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
