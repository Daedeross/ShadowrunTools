using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters
{
    public interface IViewFactory
    {
        IViewFor<T> For<T>()
            where T : class;
    }
}
