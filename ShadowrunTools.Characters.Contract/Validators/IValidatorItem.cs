namespace ShadowrunTools.Characters.Validators
{
    public interface IValidatorItem: INotifyItemChanged
    {
        bool IsValid { get; }
        string Label { get; }
        string Value { get; }
        string Message { get; }
    }
}
