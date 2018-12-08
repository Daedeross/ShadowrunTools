namespace ShadowrunTools.Characters.Validators
{
    public interface ICharacterValidator
    {
        bool Validate(ICharacter character, IRules rules);
    }
}
