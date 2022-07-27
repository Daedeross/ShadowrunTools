namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Prototypes;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public interface IDataLoader
    {
        IPrototypeRepository ReloadAll();
    }
}
