﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public abstract class ViewModelBase : NotificationObject, IDisposable
    {
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

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _displaySettings.PropertyChanged -= DisplaySettingsPropertyChanged;
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