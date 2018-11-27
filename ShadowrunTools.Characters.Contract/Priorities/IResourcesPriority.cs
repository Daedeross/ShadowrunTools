namespace ShadowrunTools.Characters.Priorities
{
    public interface IResourcesPriority
    {
        /// <summary>
        /// Amout of nuyen (¥) available to spend during creation.
        /// </summary>
        decimal Resources { get; }
    }
}
