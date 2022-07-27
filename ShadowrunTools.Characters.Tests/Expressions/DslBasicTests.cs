using Antlr4.Runtime;
using Moq;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Dsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Expressions
{
    public class DslBasicTests
    {
        public static double minus(double l, double r)
        {
            return l - r;
        }


        [Fact]
        public void ParseConstantExpressionTest()
        {
            var visitor = new DslExpressionVisitor<ITrait>();

            string exprText = "2+3";

            var str = new AntlrInputStream(exprText);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            var tree = parser.script();

            var expression = tree.Accept(visitor);

            var lambda = Expression.Lambda<Func<double>>(expression);
            var func = lambda.Compile();

            var result = func();

            Assert.Equal(5, result);
        }

        [Fact]
        public void ParserScopeExpression()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var container = new Mock<ITraitContainer>();
            container.SetupGet(x => x[It.Is<string>(s => s == "bar")])
                .Returns(obj.Object);

            var category = new Mock<ICategorizedTraitContainer>();
            category.SetupGet(x => x[It.Is<string>(s => s == "foo")])
                .Returns(container.Object);

            var scope = new Mock<IScope<ITrait>>();
            scope.SetupGet(x => x.Traits)
                .Returns(category.Object);

            var visitor = new DslExpressionVisitor<ITrait>();

            string exprText = "[foo]bar:AugmentedRating - 3";

            var str = new AntlrInputStream(exprText);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            var tree = parser.script();

            var expression = tree.Accept(visitor);

            var lambda = Expression.Lambda<Func<IScope<ITrait>, double>>(expression, visitor.Scope);
            var func = lambda.Compile();

            var result = func(scope.Object);

            Assert.Equal(2, result);
        }

        [Fact]
        public void ParserScopeSelfExpression()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var scope = new Mock<IScope<ITrait>>();
            scope.SetupGet(x => x.Me)
                .Returns(obj.Object);

            var visitor = new DslExpressionVisitor<ITrait>();

            string exprText = "me:AugmentedRating - 3";

            var str = new AntlrInputStream(exprText);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            var tree = parser.script();

            var expression = tree.Accept(visitor);

            var lambda = Expression.Lambda<Func<IScope<ITrait>, double>>(expression, visitor.Scope);
            var func = lambda.Compile();

            var result = func(scope.Object);

            Assert.Equal(2, result);
        }

        [Fact]
        public void ParseFunctionExprssion()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var container = new Mock<ITraitContainer>();
            container.SetupGet(x => x[It.Is<string>(s => s == "bar")])
                .Returns(obj.Object);

            var category = new Mock<ICategorizedTraitContainer>();
            category.SetupGet(x => x[It.Is<string>(s => s == "foo")])
                .Returns(container.Object);

            var scope = new Mock<IScope<ITrait>>();
            scope.SetupGet(x => x.Traits)
                .Returns(category.Object);

            //var minus = (new Func<double, double, double>((l, r) => l - r)).Method;
            var minus = typeof(DslBasicTests).GetMethods(BindingFlags.Static | BindingFlags.Public)[0];

            var funcs = new Dictionary<string, MethodInfo>
            {
                ["minus"] = minus
            };

            var visitor = new DslExpressionVisitor<ITrait>(funcs, new[] { typeof(ILeveledTrait), typeof(IAttribute) });

            string exprText = "minus([foo]bar:AugmentedRating, 3)";

            var str = new AntlrInputStream(exprText);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            var tree = parser.script();

            var expression = tree.Accept(visitor);

            var lambda = Expression.Lambda<Func<IScope<ITrait>, double>>(expression, visitor.Scope);
            var func = lambda.Compile();

            var result = func(scope.Object);

            Assert.Equal(2, result);

            Assert.Contains(new PropertyReference("foo", "bar", "AugmentedRating"), visitor.WatchedProperties);
        }

        [Fact]
        public void ParseTrueComparisonExprssion()
        {
            var visitor = new DslExpressionVisitor<ITrait>();

            string exprText = "2 < 3";

            var str = new AntlrInputStream(exprText);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            var tree = parser.script();

            var expression = tree.Accept(visitor);

            var lambda = Expression.Lambda<Func<bool>>(expression);
            var func = lambda.Compile();

            var result = func();

            Assert.True(result);
        }

        [Fact]
        public void ParserHasExpressionTest()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var dict1 = new Dictionary<string, ITrait> { { "bar", obj.Object } };

            var container = new Mock<ITraitContainer>();
            container.SetupGet(x => x[It.Is<string>(s => s == "bar")])
                .Returns(obj.Object);
            container.Setup(x => x.ContainsKey(It.IsAny<string>()))
                .Returns<string>(s => dict1.ContainsKey(s));

            var category = new Mock<ICategorizedTraitContainer>();
            category.SetupGet(x => x[It.Is<string>(s => s == "foo")])
                .Returns(container.Object);

            var cat = container.Object;

            category.Setup(x => x.TryGetValue(It.Is<string>(s => s == "foo"), out cat))
                .Returns(true);

            var scope = new Mock<IScope<ITrait>>();
            scope.SetupGet(x => x.Traits)
                .Returns(category.Object);

            var visitor = new DslExpressionVisitor<ITrait>();

            string exprText = "has([foo]bar)";

            var str = new AntlrInputStream(exprText);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            var tree = parser.script();

            var expression = tree.Accept(visitor);

            var lambda = Expression.Lambda<Func<IScope<ITrait>, bool>>(expression, visitor.Scope);
            var func = lambda.Compile();

            var result = func(scope.Object);

            Assert.True(result);
        }

        [Fact]
        public void ParserHasExpressionFalseTest()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var dict1 = new Dictionary<string, ITrait> { { "bar", obj.Object } };

            var container = new Mock<ITraitContainer>();
            container.SetupGet(x => x[It.Is<string>(s => s == "bar")])
                .Returns(obj.Object);
            container.Setup(x => x.ContainsKey(It.IsAny<string>()))
                .Returns<string>(s => dict1.ContainsKey(s));

            var category = new Mock<ICategorizedTraitContainer>();
            category.SetupGet(x => x[It.Is<string>(s => s == "foo")])
                .Returns(container.Object);

            var cat = container.Object;

            category.Setup(x => x.TryGetValue(It.Is<string>(s => s == "foo"), out cat))
                .Returns(true);

            var scope = new Mock<IScope<ITrait>>();
            scope.SetupGet(x => x.Traits)
                .Returns(category.Object);

            var visitor = new DslExpressionVisitor<ITrait>();

            string exprText = "has([foo]baar)";

            var str = new AntlrInputStream(exprText);
            var lexer = new CharacterBuilderLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CharacterBuilderParser(tokens);

            var tree = parser.script();

            var expression = tree.Accept(visitor);

            var lambda = Expression.Lambda<Func<IScope<ITrait>, bool>>(expression, visitor.Scope);
            var func = lambda.Compile();

            var result = func(scope.Object);

            Assert.False(result);
        }
    }
}
