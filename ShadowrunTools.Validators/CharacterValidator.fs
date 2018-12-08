
namespace ShadowrunTools.Validators

open ShadowrunTools.Characters.Validators;
open ShadowrunTools.Characters;
open ShadowrunTools.Characters.Traits

module Validators =
    let ValidateInPlayAttribute (rules: IRules) (attr: IAttribute) =
        attr.ImprovedRating <= attr.Max
        && attr.AugmentedRating - attr.ImprovedRating <= rules.MaxAugment

    let rec ValidateAttributes (rules: IRules) (attributes: IAttribute list) =
        match attributes with
        | [] -> true
        | attr :: attrs -> (ValidateInPlayAttribute rules attr) && ValidateAttributes rules attrs

    let Validate (character: ICharacter) (rules: IRules) =
        character.Attributes.Values
        |> Seq.toList
        |> (ValidateAttributes rules) 

