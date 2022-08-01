using ShadowrunTools.Characters.Model;
using System;

namespace ShadowrunTools.Characters
{
    public interface IDslParser<T>
        where T : class, INamedItem
    {
        Result<ParsedExpression<T, TRet>> ParseExpression<TRet>(string script, IScope<T> scope = null);

        Result<ParsedAugment<T>> ParseAgument(string script, IScope<T> scope = null);
    }
}
