using ShadowrunTools.Characters.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Characters
{
    public class CharacterMetatype : ICharacterMetatype
    {
        public CharacterMetatype(IMetavariantPrototype metavariant)
        {
            Name = metavariant.Name;

            _attributes = metavariant.Attributes.ToDictionary(
                proto => proto.Name,
                proto => new MetatypeAttribute(proto) as IMetatypeAttribute);
        }

        public void SetMetavariant(IMetavariantPrototype metavariant)
        {
            var newAttributes = metavariant.Attributes.ToDictionary(
                proto => proto.Name,
                proto => new MetatypeAttribute(proto) as IMetatypeAttribute);

            var joined = from newAttr in newAttributes
                         join oldAttr in _attributes on newAttr.Key equals oldAttr.Key
                         select new { Old = oldAttr.Value, New = newAttr.Value };
            var changed = (from x in joined
                           where x.Old.Max != x.New.Max || x.Old.Min != x.Old.Min
                           select x.Old.Name).ToList();

            _attributes = newAttributes;

            Name = metavariant.Name;
            changed.Add(nameof(Name));

            RaiseItemChanged(changed.ToArray());
        }

        private Dictionary<string, IMetatypeAttribute> _attributes;

        public string Name { get; private set; }

        public IMetatypeAttribute this[string name] => _attributes[name];

        public bool TryGetAttribute(string name, out IMetatypeAttribute attribute)
        {
            return _attributes.TryGetValue(name, out attribute);
        }

        #region INotifyItemChanged Implementation

        public event EventHandler<ItemChangedEventArgs> ItemChanged;

        protected void RaiseItemChanged(string[] propertyNames)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(propertyNames));
        }

        #endregion // INotifyItemChanged Implementation
    }
}
