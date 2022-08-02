using ShadowrunTools.Characters.Contract.Model;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Prototypes
{
    public interface ISkillPrototype : ILeveledTraitPrototype
    {
        bool TrainedOnly { get; }
        SkillType SkillType { get; }
        string GroupName { get; }
        string LinkedAttribute { get; }
        string UsualLimit { get; }
        List<string> Specializations { get; }
    }
}
