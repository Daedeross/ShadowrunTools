namespace ShadowrunTools.Characters
{
    using System;

    public interface ITrait: IEditable, IDisposable
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
