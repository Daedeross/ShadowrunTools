namespace ShadowrunTools.Characters.Validators
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class PointsValidator : ItemChangedBase, ICharacterValidator
    {
        private readonly IRules _rules;

        public PointsValidator(IRules rules)
        {
            _rules = rules;
        }

        public bool IsValid { get; private set; } = true;

        private List<IValidatorItem> _items = [];
        public IReadOnlyCollection<IValidatorItem> Items => _items;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}
