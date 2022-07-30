using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.ViewModels;

namespace ShadowrunTools.Characters.ViewModels
{
    /// <summary>
    /// Represents a row on the Priority Table
    /// </summary>
    public interface IPriorityRow
    {
        PriorityLevel Level { get; }

        IPriorityCell Metatype { get; }

        IPriorityCell Attributes { get; }

        IPriorityCell Specials { get; }

        IPriorityCell Skills { get; }

        IPriorityCell Resources { get; }
    }
}
