using Antlr4.Runtime;
using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowrunTools.Dsl
{
    public class DslParser<T> : IDslParser<T>
        where T : class, INamedItem
    {
        private readonly DslVisitor<T> _visitor;

        public DslParser(DslVisitor<T> visitor)
        {
            _visitor = visitor;
        }

        public Result<ParsedAugment<T>> ParseAgument(string script, IScope<T> scope = null)
        {
            var str = new AntlrInputStream(script);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            _visitor.Clear();
            var result = parser.script().Accept(_visitor);

            if (result.Type == ScriptType.Augment)
            {
                var del = Expression.Lambda<Func<IScope<T>, double>>(result.Augment.Expression, _visitor.Scope).Compile();
                Func<double> scoped = scope is null
                    ? null
                    : () => del(scope);

                return new Result<ParsedAugment<T>>
                {
                    HasValue = true,
                    Value = new ParsedAugment<T>
                    {
                        Targets = result.Augment.Targets,
                        Expression = new ParsedExpression<T, double>
                        {
                            WatchedProperties = _visitor.WatchedProperties.ToList(),
                            Delegate = del,
                            Scoped = scoped
                        }
                    }
                };
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public Result<ParsedExpression<T, TRet>> ParseExpression<TRet>(string script, IScope<T> scope = null)
        {
            try
            {
                var str = new AntlrInputStream(script);
                var lexer = new CharacterBuilderLexer(str);
                var tokens = new CommonTokenStream(lexer);
                var parser = new CharacterBuilderParser(tokens);

                _visitor.Clear();
                var result = parser.script().Accept(_visitor);

                if (result.Type == ScriptType.Expression)
                {
                    var del = Expression.Lambda<Func<IScope<T>, TRet>>(result.Expression, _visitor.Scope).Compile();
                    Func<TRet> scoped = scope is null
                        ? null
                        : () => del(scope);

                    return new Result<ParsedExpression<T, TRet>>
                    {
                        HasValue = true,
                        Value = new ParsedExpression<T, TRet>
                        {
                            WatchedProperties = _visitor.WatchedProperties.ToList(),
                            Delegate = del,
                            Scoped = scoped
                        }
                    };
                }
                else
                {
                    return new Result<ParsedExpression<T, TRet>> { HasValue = false, Message = "Invalid script." };
                }
            }
            catch (Exception e)
            {
                return new(e);
            }
        }
    }
}
