namespace ShadowrunTools.Characters
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    public class CharacterMetatype : ICharacterMetatype
    {
        public CharacterMetatype(Serialization.Prototypes.MetavariantPrototype metavariant)
        {
            _attributes = metavariant.Attributes.ToDictionary(
                proto => proto.Name,
                proto => new MetatypeAttribute(proto) as IMetatypeAttribute);
        }

        public void SetMetavariant(Serialization.Prototypes.MetavariantPrototype metavariant)
        {
            var newAttributes = metavariant.Attributes.ToDictionary(
                proto => proto.Name,
                proto => new MetatypeAttribute(proto) as IMetatypeAttribute);

            var joined = from newAttr in newAttributes
                         join oldAttr in _attributes on newAttr.Key equals oldAttr.Key
                         select new { Old = oldAttr.Value, New = newAttr.Value };
            var changed = (from x in joined
                           where x.Old.Max != x.New.Max || x.Old.Min != x.Old.Min
                           select x.Old.Name).ToArray();

            _attributes = newAttributes;

            if (changed.Any())
            {
                RaiseItemChanged(changed);
            }
        }

        private Dictionary<string, IMetatypeAttribute> _attributes;

        public IMetatypeAttribute this[string name] => _attributes[name];

        public bool TryGetAttribute(string name, out IMetatypeAttribute attribute)
        {
            return _attributes.TryGetValue(name, out attribute);
        }

        #region INotifyItemChanged Implementation

        public event ItemChangedEventHandler ItemChanged;

        protected void RaiseItemChanged(string[] propertyNames)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(propertyNames));
        }

        #endregion // INotifyItemChanged Implementation
    }
}
