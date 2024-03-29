﻿using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public interface ISkillViewModel: IViewModel<ISkill>, ILeveledTraitViewModel, ISkill
    {
        public string DisplayPool { get; }

        public string DisplaySpecializations { get; }
    }
}
