using System;
using System.IO;
using System.Linq;
using Antlr4.Runtime;

namespace ExpressionEvaluator.Parser
{
    public class ErrorListener : BaseErrorListener
    {
        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            throw new Exception(msg);
        }
    }
}
