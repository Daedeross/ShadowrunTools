using Antlr4;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDsl
{
    public class SimpleDslVisitor<TScope> : CharacterBuilderBaseVisitor<Expression>
    {
        private readonly IReadOnlyDictionary<string, MethodInfo> _functions;
        private readonly ParameterExpression _scope;

        public SimpleDslVisitor()
        {
            _scope = Expression.Parameter(typeof(TScope), "scope");
        }

        public SimpleDslVisitor(IReadOnlyDictionary<string, MethodInfo> functions)
        {
            _functions = functions;
        }

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
            return context switch
            {
                CharacterBuilderParser.ParentheticalExpressionContext ctx => VisitParentheticalExpression(ctx),
                CharacterBuilderParser.FunctionCallContext ctx            => VisitFunctionCall(ctx),
                CharacterBuilderParser.AtomicExpressionContext ctx        => VisitAtomicExpression(ctx),
                CharacterBuilderParser.PowerExpressionContext ctx         => VisitPowerExpression(ctx),
                CharacterBuilderParser.NumericUnaryExpressionContext ctx  => VisitNumericUnaryExpression(ctx),
                CharacterBuilderParser.BooleanUnaryExpressionContext ctx  => VisitBooleanUnaryExpression(ctx),
                CharacterBuilderParser.MulDivExpressionContext ctx        => VisitMulDivExpression(ctx),
                CharacterBuilderParser.AddSubExpressionContext ctx        => VisitAddSubExpression(ctx),
                CharacterBuilderParser.ComparisonExpressionContext ctx    => VisitComparisonExpression(ctx),
                CharacterBuilderParser.BooleanBinaryExpressionContext ctx => VisitBooleanBinaryExpression(ctx),
                CharacterBuilderParser.TernaryExpressionContext ctx       => VisitTernaryExpression(ctx),
                _ => throw new NotSupportedException("Unsupported expression type")
            };
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

        public override Expression VisitMulDivExpression([NotNull] CharacterBuilderParser.MulDivExpressionContext context)
        {
            var op = context.op;
            var left = VisitExpression(context.expression(0));
            var right = VisitExpression(context.expression(1));

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
            var left = VisitExpression(context.expression(0));
            var right = VisitExpression(context.expression(1));

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
            string property = context.COLON() is null ? "Rating" : context.property().GetText();

            return base.VisitVariable(context);
        }

        public override Expression VisitTrait([NotNull] CharacterBuilderParser.TraitContext context)
        {
            return base.VisitTrait(context);
        }

        public override Expression VisitTrait_type([NotNull] CharacterBuilderParser.Trait_typeContext context)
        {
            return base.VisitTrait_type(context);
        }

        public override Expression VisitTrait_name([NotNull] CharacterBuilderParser.Trait_nameContext context)
        {
            return base.VisitTrait_name(context);
        }
    }
}
