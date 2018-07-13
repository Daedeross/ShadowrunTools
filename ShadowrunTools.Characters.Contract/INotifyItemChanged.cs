using System;

namespace ShadowrunTools.Characters
{
    public interface INotifyItemChanged
    {
        event EventHandler<ItemChangedEventArgs> ItemChanged;
    }
}
