namespace ShadowrunTools.Foundation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class TextUtilities
    {
        public static string SplitPascalCase(string input)
        {
            return Regex.Replace(input, @"(?<!^)([A-Z]|[0-9]+)", @" $1");
        }
    }
}
