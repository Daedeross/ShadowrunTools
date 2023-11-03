using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Dsl
{
    public class DslAugmentVisitor<T> : CharacterBuilderBaseVisitor<IntermediateParsedAugment<T>>, IDslAugmentVisitor<T>
        where T : class, INamedItem
    {
        private readonly IDslExpressionVisitor<T> _expressionVisitor;

        public DslAugmentVisitor(IDslExpressionVisitor<T> expressionVisitor)
        {
            _expressionVisitor = expressionVisitor;
        }

        public override IntermediateParsedAugment<T> Visit(IParseTree tree)
        {
            throw new NotSupportedException();
        }

        public override IntermediateParsedAugment<T> VisitAugment([NotNull] CharacterBuilderParser.AugmentContext context)
        {
            var result = context.target().Accept(this);
            result.Expression = context.expression().Accept(_expressionVisitor);

            return result;
        }

        public override IntermediateParsedAugment<T> VisitTarget([NotNull] CharacterBuilderParser.TargetContext context)
        {
            var result = new IntermediateParsedAugment<T>();

            foreach (var target in context.variable())
            {
                result.Targets.Add(GetTarget(target));
            }

            return result;
        }

        public override IntermediateParsedAugment<T> VisitVariable([NotNull] CharacterBuilderParser.VariableContext context)
        {
            throw new NotSupportedException();
        }

        private PropertyReference GetTarget([NotNull] CharacterBuilderParser.VariableContext context)
        {
            string property = context.COLON() is null ? "AugmentedRating" : context.property().GetText();

            (string category, string name) = GetTrait(context.trait());

            return new(category, name, property);
        }

        private (string, string) GetTrait([NotNull] CharacterBuilderParser.TraitContext context)
        {
            if (context.self() != null)
            {
                return ("_me", "_me");
            }

            var category = context.trait_type().GetText();
            var name = context.trait_name().GetText();

            return (category, name);
        }
    }
}
