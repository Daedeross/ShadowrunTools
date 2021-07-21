using ShadowrunTools.Characters.Loaders;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShadowrunTools.Serialization.Loaders
{
    public abstract class LoaderBase
    {
        [DataMember]
        public int PrototypeHash { get; set; }
    }
}
