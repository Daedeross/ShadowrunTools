﻿using System;
using System.ComponentModel;

namespace ShadowrunTools.Characters
{
    /// <summary>
    /// Bonus/penalty from an <see cref="IAugment"/> as applied to a Trait
    /// </summary>
    public interface IBonus: INotifyPropertyChanged, IDisposable
    {
        IAugment Source { get; }

        double Amount { get; }

        string TargetProperty { get; }
    }
}
