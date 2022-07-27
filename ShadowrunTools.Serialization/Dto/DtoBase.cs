using ShadowrunTools.Characters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShadowrunTools.Serialization
{
    public abstract class DtoBase
    {
        [DataMember]
        public int PrototypeHash { get; set; }
    }
}
