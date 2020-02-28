using System;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Validators
{
    public class ValidatorItem : ItemChangedBase, IValidatorItem, IDisposable
    {
        private readonly INotifyItemChanged _watch;
        private readonly ISet<string> _propertyNames;
        private readonly Func<INotifyItemChanged, bool> _validation;

        private bool mIsValid;
        public bool IsValid
        {
            get => mIsValid;
            set
            {
                if (RaiseAndSetIfChanged(ref mIsValid, value))
                {
                    RaiseItemChanged(nameof(Message));
                }
            }
        }

        private string mLabel;
        public string Label
        {
            get => mLabel;
            set => RaiseAndSetIfChanged(ref mLabel, value);
        }

        private string mValue;
        public string Value
        {
            get => mValue;
            set => RaiseAndSetIfChanged(ref mValue, value);
        }

        private string mMessage;
        public string Message
        {
            get => IsValid ? null : mMessage;
        }

        public ValidatorItem(INotifyItemChanged watch,
                             string label,
                             string message,
                             Func<INotifyItemChanged, bool> validation,
                             IEnumerable<string> propertyNames)
        {
            _watch = watch;
            mLabel = label;
            mMessage = message;
            _validation = validation;
            _propertyNames = new HashSet<string>(propertyNames);

            _watch.ItemChanged += OnCharacterChanged;
        }

        public void OnCharacterChanged(object sender, ItemChangedEventArgs e)
        {
            if (_propertyNames.Overlaps(e.PropertyNames))
            {
                IsValid = _validation(_watch);
            };
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _watch.ItemChanged -= OnCharacterChanged;
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
