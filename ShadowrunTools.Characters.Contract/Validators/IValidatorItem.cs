namespace ShadowrunTools.Characters.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IValidatorItem
    {
        bool IsValid { get; }
        string Label { get; }
        string Value { get; }
        string Message { get; }
    }
}
