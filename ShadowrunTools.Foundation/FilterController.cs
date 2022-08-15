using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Foundation
{
    public class FilterController<T> : IDisposable
    {

        private IObservable<Func<T, bool>> _filter;
        public IObservable<Func<T, bool>> Filter => _filter;

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //_filter.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
