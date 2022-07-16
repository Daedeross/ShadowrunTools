namespace ShadowrunTools.Characters.Traits
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IHaveTaboo : ISelfValidate
    {
        string Taboo { get; set; }
    }
}
