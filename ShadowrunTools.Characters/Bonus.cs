using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowrunTools.Characters
{
    public class Bonus : IBonus
    {
        private static readonly Dictionary<string, string> _synonyms = new Dictionary<string, string>
        {
            { "AugmentedRating", "Rating" }
        };

        public Bonus(IAugment source, string targetProperty)
        {
            Source = source;
            Source.PropertyChanged += OnAugmentChanged;
            _targetProperty = GetTruePropertyName(targetProperty);
        }

        public IAugment Source { get; }

        public double Amount => Source.Amount;

        private string _targetProperty;

        public string TargetProperty
        {
            get => _targetProperty;
            set
            {
                var newName = GetTruePropertyName(value);
                if (!string.Equals(_targetProperty, newName))
                {
                    _targetProperty = newName;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnAugmentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAugment.Amount))
            {
                NotifyPropertyChanged(nameof(Amount));
            }
        }

        private static string GetTruePropertyName(string propertyName)
        {
            string result;
            if(_synonyms.TryGetValue(propertyName, out string trueName))
            {
                result = trueName;
            }
            else
            {
                result = propertyName;
            }

            if (result.StartsWith("Bonus"))
            {
                return result;
            }
            else
            {
                return $"Bonus{result}";
            }
        }

        #region IDisposable

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Source.PropertyChanged -= OnAugmentChanged;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }

        #endregion
    }
}
