using Antlr4.Runtime.Tree;
using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ShadowrunTools.Dsl
{
    public class DslVisitor<T> : CharacterBuilderBaseVisitor<ParsedScript<T>>
    {
        private readonly DslExpressionVisitor<T> _expressionVisitor;
        private readonly DslAugmentVisitor<T> _augmentVisitor;

        public IEnumerable<PropertyReference> WatchedProperties => _expressionVisitor.WatchedProperties;

        public ParameterExpression Scope => _expressionVisitor.Scope;

        public DslVisitor(DslExpressionVisitor<T> expressionVisitor, DslAugmentVisitor<T> augmentVisitor)
        {
            _expressionVisitor = expressionVisitor;
            _augmentVisitor = augmentVisitor;
        }

        public void Clear()
        {
            _expressionVisitor._watchedProperties.Clear();
        }


        public override ParsedScript<T> Visit(IParseTree tree)
        {
            return base.Visit(tree);
        }

        public override ParsedScript<T> VisitScript([NotNull] CharacterBuilderParser.ScriptContext context)
        {
            var aug = context.augment();
            var expr = context.expression();

            if (aug is not null)
            {
                return new ParsedScript<T>(aug.Accept(_augmentVisitor));
            }
            else if (expr is not null)
            {
                return new ParsedScript<T>(expr.Accept(_expressionVisitor));
            }
            else
            {
                throw new InvalidOperationException("Unknown script context");
            }
        }
    }
}
