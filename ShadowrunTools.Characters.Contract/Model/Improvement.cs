namespace ShadowrunTools.Characters.Model
{
    using System;

    public record class Improvement (Guid Id, ImprovementSource Source, int OldValue, int NewValue, int Cost);
}
