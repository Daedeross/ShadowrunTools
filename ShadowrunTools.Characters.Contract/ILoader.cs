namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Prototypes;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITraitLoader<TTrait, TPrototype>
        where TTrait: class
        where TPrototype: class

    {
        TTrait ToClass(IPrototypeRepository prototypes);
    }
}
