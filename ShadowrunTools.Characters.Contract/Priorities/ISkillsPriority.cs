namespace ShadowrunTools.Characters.Priorities
{
    public interface ISkillsPriority
    {

        /// <summary>
        /// Max sum for all active skill ratings (non-grouped).
        /// </summary>
        int SkillPoints { get; }

        /// <summary>
        /// Max sum for all active skill groups.
        /// </summary>
        int SkillGroupPoints { get; }
    }
}
