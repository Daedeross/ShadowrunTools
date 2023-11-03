using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShadowrunTools.Dsl
{
    public interface IDslExpressionVisitor<T>: ICharacterBuilderVisitor<Payload<Expression>>
        where T : class, INamedItem
    {
        ICollection<PropertyReference> WatchedProperties { get; }

        ParameterExpression Scope { get; }
    }
}
