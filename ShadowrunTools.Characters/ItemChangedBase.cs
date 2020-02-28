using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ShadowrunTools.Characters
{
    public abstract class ItemChangedBase : INotifyItemChanged
    {
        public event EventHandler<ItemChangedEventArgs> ItemChanged;

        protected void RaiseItemChanged(params string[] propertyNames)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(propertyNames));
        }

        protected TRet RaiseAndSetIfChanged<TRet>(
            ref TRet backingField,
            TRet newValue,
            [CallerMemberName] string propertyName = null,
            IEqualityComparer<TRet> equalityComparer = null)
        {
            var comparer = equalityComparer ?? EqualityComparer<TRet>.Default;
            if (!comparer.Equals(backingField, newValue))
            {
                backingField = newValue;
                RaiseItemChanged(propertyName);
            }

            return newValue;
        }
    }
}
