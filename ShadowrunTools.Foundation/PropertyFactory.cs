using System;
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

            foreach (var kvp in propList.Where(p => p.Value.Editable))
            {
                if (props.TryGetValue(kvp.Key, out var pi))
                {
                    pi.SetValue(obj, kvp.Value.Value);
                }
                else
                {
                    Console.WriteLine($"Warning: Unknown property '{kvp.Key}'.");   // TODO: ILogger
                }
            }
        }
    }
}
