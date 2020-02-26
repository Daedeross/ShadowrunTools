using ReactiveUI;

namespace ShadowrunTools.Characters.ViewModels
{
    public class StatusItemViewModel : ViewModelBase
    {
        private string mLabel;
        public string Label
        {
            get { return mLabel; }
            set { this.RaiseAndSetIfChanged(ref mLabel, value); }
        }

        private string mText;
        public string Text
        {
            get { return mText; }
            set { this.RaiseAndSetIfChanged(ref mText, value); }
        }

        private string mTooltip;
        public string Tooltip
        {
            get { return mTooltip; }
            set { this.RaiseAndSetIfChanged(ref mTooltip, value); }
        }

        public StatusItemViewModel(DisplaySettings displaySettings)
            : base(displaySettings)
        {
        }
    }
}
