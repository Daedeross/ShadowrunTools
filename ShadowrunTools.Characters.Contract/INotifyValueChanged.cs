namespace ShadowrunTools.Characters
{
    public interface INotifyValueChanged
    {
        event ValueChangedEventHandler ValueChanged;

        void RaiseValueChanged(ValueChangedEventArgs args);
    }
}
