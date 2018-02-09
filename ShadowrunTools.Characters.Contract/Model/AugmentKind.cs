namespace ShadowrunTools.Characters.Model
{
    /// <summary>
    /// <em>How</em> the Augment enhances the target trait.
    /// Essentialy what property it effects.
    /// </summary>
    public enum AugmentKind
    {
        /// <summary>Augment does nothing!?</summary>
        None = 0,
        /// <summary>Augment alters the target trait's <b>Rating.</b></summary>
        Rating,
        /// <summary>Changes the trait's innate Max</summary>
        Max,
        /// <summary>Augment alters the target trait's <b>Damage Value.</b></summary>
        DamageValue,
        /// <summary>Augment alters the target trait's <b>Damage Type</b> (i.e. Stun ↔ Physical).</summary>
        DamageType,
        /// <summary>Augment alters the target trait's <b>Accuracy</b> (or Limit).</summary>
        Accuracy,
        /// <summary>Augment alters the target trait's <b>Availability</b> rating</summary>
        Availability,
        /// <summary>>Augment alters the target trait's <b>Restriction</b> (eg R or F).</summary>
        Restriction,
        /// <summary>>Augment alters the target trait's <b>Recoil Compensation</b>.</summary>
        RC,
        /// <summary>Augment alters the target trait's <b>Armor Penetration</b> rating.</summary>
        AP,
    }
}
