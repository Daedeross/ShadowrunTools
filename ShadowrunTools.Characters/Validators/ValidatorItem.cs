namespace ShadowrunTools.Characters.Validators
{
    public class ValidatorItem : ItemChangedBase, IValidatorItem
    {
        private bool mIsValid;
        public bool IsValid
        {
            get => mIsValid;
            set => RaiseAndSetIfChanged(ref mIsValid, value);
        }

        private string mLabel;
        public string Label
        {
            get => mLabel;
            set => RaiseAndSetIfChanged(ref mLabel, value);
        }

        private string mValue;
        public string Value
        {
            get => mValue;
            set => RaiseAndSetIfChanged(ref mValue, value);
        }

        private string mMessage;
        public string Message
        {
            get => mMessage;
            set => RaiseAndSetIfChanged(ref mMessage, value);
        }
    }
}
