using System.Text.RegularExpressions;

namespace ShadowrunTools.Foundation
{

    public static class TextUtilities
    {
        public static string SplitPascalCase(string input)
        {
            return Regex.Replace(input, @"(?<!^)([A-Z]|[0-9]+)", @" $1");
        }
    }
}
