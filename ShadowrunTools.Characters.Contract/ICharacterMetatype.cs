namespace ShadowrunTools.Characters
{
    public interface ICharacterMetatype : INotifyItemChanged
    {
        string Name { get; }
        IMetatypeAttribute this[string name] { get; }
        bool TryGetAttribute(string name, out IMetatypeAttribute attribute);
    }
}
