namespace ShadowrunTools.Characters.Traits
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAttribute: ILeveledTrait
    {
        string ShortName { get; }
    }
}
