namespace ShadowrunTools.Characters.ViewModels
{
    using ShadowrunTools.Characters.Validators;

    public class ValidatorItemViewModel : RecordViewModelBase<IValidatorItem>
    {
        public ValidatorItemViewModel(DisplaySettings displaySettings, IValidatorItem record)
            : base(displaySettings, record)
        {
        }

        public bool IsValid => Record.IsValid;
        public string Label => Record.Label;
        public string Value => Record.Value;
        public string Message => Record.Message;
    }
}
