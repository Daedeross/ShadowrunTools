using ReactiveUI;
using ShadowrunTools.Characters.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Characters.Wpf.ViewModel
{
    public class ViewContainer : ReactiveObject, IViewContainer, IDisposable
    {
        private bool disposedValue;

        public ViewContainer(string title, IDocumentViewModel content, bool ownsContent = false)
            : this(title, content, ownsContent, Guid.NewGuid())
        {

        }

        public ViewContainer(string title, IDocumentViewModel content, bool ownsContent, Guid id)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));
            _content = content ?? throw new ArgumentNullException(nameof(content));
            Id = id;
            OwnsContent = ownsContent;

            _content.PropertyChanged += OnContentChanged;
        }

        public Guid Id { get; }

        public bool OwnsContent { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public IDocumentViewModel _content;
        public IViewModel Content => _content;

        #region IDisposable

        private void OnContentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender == _content && (e.PropertyName == "Name" || e.PropertyName == "Title"))
            {
                Title = _content.Name;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _content.PropertyChanged -= OnContentChanged;
                    if (OwnsContent && Content is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
