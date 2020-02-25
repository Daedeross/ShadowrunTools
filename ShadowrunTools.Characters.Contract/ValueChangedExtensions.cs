namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ValueChangedExtensions
    {
        public static bool RaiseAndSetIfChanged<TObj, TRet>(this TObj targetObject, ref TRet backingField, TRet newValue, [CallerMemberName] string propertyName = null, IEqualityComparer<TRet> equalityComparer = default) where TObj : INotifyValueChanged
        {
            if (!equalityComparer.Equals(backingField, newValue))
            {
                var args = new ValueChangedEventArgs(propertyName, backingField, newValue);
                backingField = newValue;
                targetObject.RaiseValueChanged(args);
                return true;
            }

            return false;
        }
    }
}
