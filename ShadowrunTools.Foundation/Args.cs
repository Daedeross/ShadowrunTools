using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Foundation
{
    public static class Args
    {
        public static void NotNull(object argument, string name)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
