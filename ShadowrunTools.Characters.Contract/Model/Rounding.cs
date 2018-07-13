namespace ShadowrunTools.Characters.Model
{
    /// <summary>
    /// Rule for how to round a calculated value.
    /// </summary>
    public enum Rounding
    {
        AwayFromZero = 0,
        ToEven       = 1,
        Up           = 2,
        Down         = 3,
    }
}
