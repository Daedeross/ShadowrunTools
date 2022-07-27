using ShadowrunTools.Characters.Model;
using System.Linq.Expressions;

namespace ShadowrunTools.Dsl
{
    public class ParsedScript<T>
    {
        public ScriptType Type { get; private set; }

        public IntermediateParsedAugment<T> Augment { get; private set; }

        public Expression Expression { get; private set; }

        public ParsedScript(IntermediateParsedAugment<T> augment)
        {
            this.Type = ScriptType.Augment;
            Augment = augment;
            Expression = null;
        }

        public ParsedScript(Expression expression)
        {
            this.Type = ScriptType.Expression;
            Augment = null;
            Expression = expression;
        }
    }
}
