namespace ShadowrunTools.Characters.Priorities
{
    /// <summary>
    /// Represents an option on the "Metatype" column of the Priority Table.
    /// </summary>
    public interface IPriorityMetavariantOption
    {
        /// <summary>
        /// The name of the metavariant.
        /// </summary>
        string Metavariant { get; }

        /// <summary>
        /// Points given to spend on Edge and Magic -or- Resonance.
        /// </summary>
        int SpecialAttributePoints { get; }

        /// <summary>
        /// Additional Karma cost for picking this option.
        /// </summary>
        int AdditionalKarmaCost { get; }
    }
}
