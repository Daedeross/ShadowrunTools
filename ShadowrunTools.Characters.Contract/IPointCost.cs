namespace ShadowrunTools.Characters.Contract
{
    public interface IPointCost
    {
        /// <summary>
        /// The cost of the trait if using BP or Priority rules.
        /// </summary>
        int Points { get; }
    }
}
