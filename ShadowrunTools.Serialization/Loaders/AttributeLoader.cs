using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "Attribute", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class AttributeLoader: LeveledTraitLoader
    {
    }
}
