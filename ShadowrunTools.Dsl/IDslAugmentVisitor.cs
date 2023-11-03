using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Dsl
{
    public interface IDslAugmentVisitor<T>: ICharacterBuilderVisitor<IntermediateParsedAugment<T>>
        where T : class, INamedItem
    {
    }
}
