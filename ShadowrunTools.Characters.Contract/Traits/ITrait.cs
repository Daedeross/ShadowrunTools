namespace ShadowrunTools.Characters.Traits
{
    using System;

    public interface ITrait: IEditable, IDisposable, IEquatable<ITrait>
    {
        Guid Id { get; }
        string Name { get; set; }
        string Category { get; }
        string SubCategory { get; set; }
        string UserNotes { get; set; }

        #region Reference Info
        string Book { get; set; }
        int Page { get; set; }
        #endregion // Reference Info
    }
}
