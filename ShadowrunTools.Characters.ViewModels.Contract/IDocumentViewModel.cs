﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface IDocumentViewModel : IViewModel
    {
        public string Name { get; set; }
    }
}
