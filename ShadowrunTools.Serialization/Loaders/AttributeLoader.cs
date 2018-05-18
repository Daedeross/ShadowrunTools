namespace ShadowrunTools.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "Attribute", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class AttributeLoader: LeveledTraitLoader
    {
    }
}
