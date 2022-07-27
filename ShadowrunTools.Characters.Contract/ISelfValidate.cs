using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters
{
    public interface ISelfValidate
    {
        bool Invalid { get; }
        string InvalidMessage { get; }
    }
}
