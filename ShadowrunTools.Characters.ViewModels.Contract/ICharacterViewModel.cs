using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ICharacterViewModel : IDocumentViewModel
    {
        string Name { get; set; }
    }
}
