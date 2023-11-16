namespace ShadowrunTools.Characters.Traits
{
    public interface IHavePrerequisite : ISelfValidate
    {
        string Needs { get; set; }
    }
}
