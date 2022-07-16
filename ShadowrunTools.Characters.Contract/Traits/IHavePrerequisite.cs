using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.Traits
{
    public interface IHavePrerequisite : ISelfValidate
    {
        string Needs { get; set; }
    }
}
