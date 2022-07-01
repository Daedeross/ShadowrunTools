using Antlr4;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Dsl
{
    public class CharacterBuilderDslVisitor<TTrait> : CharacterBuilderBaseVisitor<Expression>
    {
        private readonly IReadOnlyDictionary<string, MethodInfo> _functions;
        private readonly ParameterExpression _scope;
        private readonly Expression _me;
        private readonly Expression _categories;
        private readonly PropertyInfo _categoriesIndexer;
        private readonly PropertyInfo _traitsIndexer;
        private readonly IEnumerable<Type> _traitTypes;

        public CharacterBuilderDslVisitor()
            : this(new Dictionary<string, MethodInfo>(),
                  new[] { typeof(ILeveledTrait), typeof(IAttribute)} )
        {
        }

        public CharacterBuilderDslVisitor(IReadOnlyDictionary<string, MethodInfo> functions,
            Type[] otherTypes)
        {
            _functions = functions;
            var type = typeof(IScope<TTrait>);
            _scope = Expression.Parameter(type, "scope");
            _me = Expression.Property(_scope, type.GetProperty(nameof(IScope<TTrait>.Me)));
            _categories = Expression.Property(_scope, type.GetProperty(nameof(IScope<TTrait>.Traits)));
            _categoriesIndexer = typeof(IDictionary<string, ITraitContainer>).GetProperty("Item");
            _traitsIndexer = typeof(IDictionary<string, ITrait>).GetProperty("Item");

            _traitTypes = otherTypes;
        }

        public ParameterExpression Scope => _scope;

        public override Expression Visit(IParseTree tree)
        {
            return base.Visit(tree);
        }

        public override Expression VisitScript([NotNull] CharacterBuilderParser.ScriptContext context)
        {
            var aug = context.augment();
            var expr = context.expression();

            if (aug is not null)
            {
                return VisitAugment(aug);
            }
            else if (expr is not null)
            {
                return VisitExpression(expr);
            }
            else
            {
                throw new InvalidOperationException("Unknown script context");
            }
        }

        public Expression VisitExpression([NotNull] CharacterBuilderParser.ExpressionContext context)
        {
            return context.Accept(this);
        }

        public override Expression VisitParentheticalExpression([NotNull] CharacterBuilderParser.ParentheticalExpressionContext context)
        {
            return VisitExpression(context.expression());
        }

        public override Expression VisitFunctionCall([NotNull] CharacterBuilderParser.FunctionCallContext context)
        {
            return base.VisitFunction(context.function());
        }

        public override Expression VisitAtomicExpression([NotNull] CharacterBuilderParser.AtomicExpressionContext context)
        {
            return VisitAtom(context.atom());
        }

        public override Expression VisitPowerExpression([NotNull] CharacterBuilderParser.PowerExpressionContext context)
        {
            var left = VisitExpression(context.expression(0));
            var right = VisitExpression(context.expression(1));
            return Expression.Power(left, right);
        }

        public override Expression VisitNumericUnaryExpression([NotNull] CharacterBuilderParser.NumericUnaryExpressionContext context)
        {
            var expr = VisitExpression(context.expression());

            return context.op.Type switch
            {
                CharacterBuilderParser.PLUS => expr,
                CharacterBuilderParser.MINUS => Expression.Negate(expr),
                _ => throw new InvalidOperationException($"Unrecognized unary operator {context.op.Text}")
            };
        }

        public override Expression VisitBooleanUnaryExpression([NotNull] CharacterBuilderParser.BooleanUnaryExpressionContext context)
        {
            return Expression.Not(VisitExpression(context.expression()));
        }

        private Expression MakeDouble(Expression expression)
        {
            if (expression.Type == typeof(double))
            {
                return expression;
            }

            return Expression.Convert(expression, typeof(double));
        }

        public override Expression VisitMulDivExpression([NotNull] CharacterBuilderParser.MulDivExpressionContext context)
        {
            var op = context.op;
            var left = MakeDouble(VisitExpression(context.expression(0)));
            var right = MakeDouble(VisitExpression(context.expression(1)));

            return op.Type switch
            {
                CharacterBuilderParser.TIMES => Expression.Multiply(left, right),
                CharacterBuilderParser.DIV => Expression.Divide(left, right),
                _ => throw new InvalidOperationException($"Unrecognized binary operator {op.Text}")
            };
        }

        public override Expression VisitAddSubExpression([NotNull] CharacterBuilderParser.AddSubExpressionContext context)
        {
            var op = context.op;
            var left =  MakeDouble(VisitExpression(context.expression(0)));
            var right = MakeDouble(VisitExpression(context.expression(1)));

            return op.Type switch
            {
                CharacterBuilderParser.PLUS => Expression.Add(left, right),
                CharacterBuilderParser.MINUS => Expression.Subtract(left, right),
                _ => throw new InvalidOperationException($"Unrecognized binary operator {op.Text}")
            };
        }

        public override Expression VisitComparisonExpression([NotNull] CharacterBuilderParser.ComparisonExpressionContext context)
        {
            var op = context.op;
            var left = VisitExpression(context.expression(0));
            var right = VisitExpression(context.expression(1));

            return op.Type switch
            {
                CharacterBuilderParser.EQ => Expression.Equal(left, right),
                CharacterBuilderParser.GT => Expression.GreaterThan(left, right),
                CharacterBuilderParser.LT => Expression.LessThan(left, right),
                CharacterBuilderParser.GEQ => Expression.GreaterThanOrEqual(left, right),
                CharacterBuilderParser.LEQ => Expression.LessThanOrEqual(left, right),
                _ => throw new InvalidOperationException($"Unrecognized binary operator {op.Text}")
            };
        }

        public override Expression VisitBooleanBinaryExpression([NotNull] CharacterBuilderParser.BooleanBinaryExpressionContext context)
        {
            var op = context.op;
            var left = VisitExpression(context.expression(0));
            var right = VisitExpression(context.expression(1));

            return op.Type switch
            {
                CharacterBuilderParser.AND => Expression.AndAlso(left, right),
                CharacterBuilderParser.OR => Expression.OrElse(left, right),
                _ => throw new InvalidOperationException($"Unrecognized binary operator {op.Text}")
            };
        }

        public override Expression VisitTernaryExpression([NotNull] CharacterBuilderParser.TernaryExpressionContext context)
        {
            var condition = VisitExpression(context.expression(0));
            var if_true = VisitExpression(context.expression(1));
            var if_false = VisitExpression(context.expression(2));

            return Expression.IfThenElse(condition, if_true, if_false);
        }

        public override Expression VisitAtom([NotNull] CharacterBuilderParser.AtomContext context)
        {
            return context.children[0] switch
            {
                CharacterBuilderParser.Numeric_literalContext ctx => VisitNumeric_literal(ctx),
                CharacterBuilderParser.String_literalContext ctx => VisitString_literal(ctx),
                CharacterBuilderParser.VariableContext ctx => VisitVariable(ctx),
                _ => throw new InvalidOperationException($"Unknown atomic expression '{context.GetText()}'")
            };
        }

        public override Expression VisitString_literal([NotNull] CharacterBuilderParser.String_literalContext context)
        {
            var str = context.GetText().Trim('\'', '"');

            return Expression.Constant(str, typeof(string));
        }

        public override Expression VisitNumeric_literal([NotNull] CharacterBuilderParser.Numeric_literalContext context)
        {
            var text = context.GetText();

            /// All numeric types are treated as <see cref="double"/> until needed otherwise.
            if (double.TryParse(text, out var val))
            {
                return Expression.Constant(val, typeof(double));
            }
            else
            {
                throw new InvalidCastException($"Unable to parse {text} as a number");
            }
        }

        public override Expression VisitBoolean_literal([NotNull] CharacterBuilderParser.Boolean_literalContext context)
        {
            return base.VisitBoolean_literal(context);
        }

        public override Expression VisitFunction([NotNull] CharacterBuilderParser.FunctionContext context)
        {
            var fName = context.NAME().GetText();

            if (!_functions.TryGetValue(fName, out var methodInfo))
            {
                throw new IndexOutOfRangeException($"'{fName}' is not a defined function");
            }

            var args = GetArgList(context.argList());

            return Expression.Call(methodInfo, args);
        }

        public override Expression VisitArgList([NotNull] CharacterBuilderParser.ArgListContext context)
        {
            throw new NotSupportedException();
        }

        private Expression[] GetArgList([NotNull] CharacterBuilderParser.ArgListContext context)
        {
            var count = context.ChildCount;
            var args = new Expression[count];

            for (int i = 0; i < count; i++)
            {
                var child = context.children[i].Accept(this);
                args[i] = child;
            }

            return args;
        }

        public override Expression VisitVariable([NotNull] CharacterBuilderParser.VariableContext context)
        {
            string property = context.COLON() is null ? "AugmentedRating" : context.property().GetText();

            var trait = context.trait().Accept(this);

            if (trait.Type.GetProperty(property) is null)
            {
                foreach (var type in _traitTypes.Where(t => typeof(ITrait).IsAssignableFrom(t)))
                {
                    var pi = type.GetProperty(property);
                    if (pi is not null)
                    {
                        return Expression.Property(Expression.Convert(trait, type), pi);
                    }
                }
            }

            return Expression.Property(trait, property);
        }

        public override Expression VisitTrait([NotNull] CharacterBuilderParser.TraitContext context)
        {
            var category = context.trait_type().Accept(this);
            var name = context.trait_name().Accept(this);

            return Expression.MakeIndex(category, _traitsIndexer, new[] { name } );
        }

        public override Expression VisitTrait_type([NotNull] CharacterBuilderParser.Trait_typeContext context)
        {
            var name = Expression.Constant(context.GetText(), typeof(string));
            return Expression.MakeIndex(_categories, _categoriesIndexer, new[] { name });
        }

        public override Expression VisitTrait_name([NotNull] CharacterBuilderParser.Trait_nameContext context)
        {
            return Expression.Constant(context.GetText(), typeof(string));
        }

        public override Expression VisitSelf([NotNull] CharacterBuilderParser.SelfContext context)
        {
            return _me;
        }

        public override Expression VisitAncestor([NotNull] CharacterBuilderParser.AncestorContext context)
        {
            return base.VisitAncestor(context);
        }

        public override Expression VisitAugment([NotNull] CharacterBuilderParser.AugmentContext context)
        {
            throw new NotSupportedException();
        }
    }
}
