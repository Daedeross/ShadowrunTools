namespace ShadowrunTools.Characters
{
    using System;

    public interface INotifyItemChanged
    {
        event EventHandler<ItemChangedEventArgs> ItemChanged;
    }
}
