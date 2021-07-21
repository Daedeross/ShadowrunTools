namespace ShadowrunTools.Characters.Prototypes
{
    /// <summary>
    /// Base interface for all prototypes.
    /// </summary>
    /// <remarks>
    /// Prototypes are what define what is available to a charcater. They are the data that 
    /// is used to create the on-character stuff, like traits and priorities.
    /// They define things like name, min &  max levels, augments, restrictions, etc on traits.
    /// You can think of them like OOP classes.
    /// 
    /// The base interface will probably remain empty.
    /// </remarks>
    public interface IPrototype
    {
        int GetHashCode();
    }
}
