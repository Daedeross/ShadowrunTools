using ShadowrunTools.Characters.Model;
using ShadowrunTools.Dsl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShadowrunTools.Characters
{
    public interface IDslVisitor<T>: ICharacterBuilderVisitor<ParsedScript<T>>
        where T : class, INamedItem
    {
        ParameterExpression Scope { get; }

        public ICollection<PropertyReference> WatchedProperties { get; }

        void Clear();
    }
}
