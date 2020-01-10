using ShadowrunTools.Characters.Priorities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization.Prototypes.Priorities
{
    [DataContract(Name = "SpecialsPriorityPrototype", Namespace = "http://schemas.shadowruntools.com/prototypes")]
    public class SpecialsPriorityPrototype : ISpecialsPriority
    {
        #region Serialized
        #pragma warning disable IDE1006 // Naming Styles
        [DataMember(Name = "Options")]
        public List<SpecialOptionPrototype> _Options { get; set; } = new List<SpecialOptionPrototype>();
        #pragma warning restore IDE1006 // Naming Styles
        #endregion

        public IReadOnlyCollection<ISpecialOption> Options => _Options;
    }
}
