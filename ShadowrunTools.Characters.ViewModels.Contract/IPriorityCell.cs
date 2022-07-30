using System.Collections.Generic;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface IPriorityCell
    {
        List<string> Items { get; }
        string VisibleItems { get; }
        bool IsSelected { get; set; }
    }
}
