using System.Collections.Generic;

namespace ShadowrunTools.Characters.Model
{
    public class ParsedAugment<T>
    {
        public List<PropertyReference> Targets { get; set; } = new List<PropertyReference>();
        public ParsedExpression<T, double> Expression { get; set; }
    }
}
