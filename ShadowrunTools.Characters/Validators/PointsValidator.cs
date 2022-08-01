namespace ShadowrunTools.Characters.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;

    public class PointsValidator : ItemChangedBase, ICharacterValidator
    {
        private readonly IRules _rules;

        public PointsValidator(IRules rules)
        {
            _rules = rules;
        }

        public bool IsValid { get; private set; } = true;

        private List<IValidatorItem> _items = new();
        public IReadOnlyCollection<IValidatorItem> Items => _items;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
