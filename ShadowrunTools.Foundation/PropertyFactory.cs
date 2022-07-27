using System;
using System.Collections.Concurrent;
using System.Linq;

namespace ShadowrunTools.Foundation
{
    public static class PropertyFactory
    {
        public static IPropertyList CreateFromObject(object obj, bool inherit = false)
        {
            var type = obj.GetType();

            var props = type.GetProperties();

            IPropertyList dict = new PropertyList(props.Length);

            foreach (var prop in props)
            {
                if (prop.GetCustomAttributes(typeof(DisplayAttribute), inherit).FirstOrDefault() is DisplayAttribute displayAttribute)
                {
                    string label;
                    if (string.IsNullOrWhiteSpace(displayAttribute.Label))
                    {
                        label = TextUtilities.SplitPascalCase(prop.Name);
                    }
                    else
                    {
                        label = displayAttribute.Label;
                    }
                    dict.Add(prop.Name, new Property(label, prop.GetValue(obj), prop.PropertyType, displayAttribute.Editable));
                }
            }

            return dict;
        }

        public static void SetFromPropertyList(object obj, IPropertyList propList)
        {
            var type = obj.GetType();

            var props = type.GetProperties()
                .ToDictionary(pi => pi.Name, pi => pi);

            foreach (var prop in propList.Where(p => p.Value.Editable))
            {
                props[prop.Key].SetValue(obj, prop.Value);
            }
        }
    }
}
