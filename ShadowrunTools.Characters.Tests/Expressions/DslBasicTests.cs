using Antlr4.Runtime;
using Moq;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Dsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Expressions
{
    public class DslBasicTests
    {
        [Fact]
        public void ParseConstantExpressionTest()
        {
            var visitor = new CharacterBuilderDslVisitor<ITrait>();

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

            var visitor = new CharacterBuilderDslVisitor<ITrait>();

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
    }
}
