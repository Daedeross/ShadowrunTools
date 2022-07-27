using ShadowrunTools.Characters.Model;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShadowrunTools.Dsl
{
    public class IntermediateParsedAugment<T>
    {
        public List<PropertyReference> Targets { get; set; } = new List<PropertyReference>();
        public Expression Expression { get; set; }
    }
}
