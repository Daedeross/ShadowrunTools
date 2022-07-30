using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.Traits
{
    public interface ISkillGroup : ILeveledTrait
    {
        IEnumerable<string> Skills { get; set; }

        bool Broken { get; }
    }
}
