using System;

namespace ShadowrunTools.Characters
{
    public abstract class ItemChangedBase : INotifyItemChanged
    {
        public event EventHandler<ItemChangedEventArgs> ItemChanged;

        protected void RaiseItemChanged(string[] propertyNames)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(propertyNames));
        }
    }
}
