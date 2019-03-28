using ShadowrunTools.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public class DisplaySettings : NotificationObject
    {
        public DisplaySettings()
        {
        }

        [Display(Editable = true, Label = "Priority Cell Visible Items Count")]
        public int PriorityCellVisibleItemsCount { get; set; } = 2;
    }
}
